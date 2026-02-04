using Examine;
using Microsoft.Extensions.Logging;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Web.Review.Search.Core;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Web.Review.Search.BackgroundTask;

public class ReviewSearchBackgroundTask(
    ICoreScopeProvider scopeProvider,
    IKeyValueRepository keyValueRepository,
    IContentService contentService,
    IExamineManager examineManager,
    ReviewIndexValueSetBuilder editorIndexValueSetBuilder,
    ILogger<ReviewSearchBackgroundTask> logger)
    : IReviewSearchBackgroundTask
{
    private const string KeyValueSchedule = nameof(ReviewSearchBackgroundTask) + ":" + nameof(KeyValueSchedule) + ":";
    private const int AllContent = Constants.System.Root;
    private const string DeleteValue = "";

    public bool ScheduleExectionOfAllContent() => ScheduleExection(AllContent);

    public bool ScheduleExection(int parentId)
    {
        using (var scope = scopeProvider.CreateCoreScope(System.Data.IsolationLevel.ReadCommitted))
        {
            try
            {
                var repo = keyValueRepository.Get(KeyValueSchedule + parentId.ToString()) ?? new KeyValue
                {
                    Identifier = KeyValueSchedule + parentId.ToString()
                };
                repo.Value = parentId.ToString();
                repo.UpdateDate = DateTime.UtcNow;
                keyValueRepository.Save(repo);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                scope.Complete();
            }
        }

        if (parentId != AllContent && examineManager.TryGetIndex(nameof(ReviewIndex), out IIndex? index))
        {
            UpdateContent(index, parentId, CurrentReviewDate(parentId), CurrentPathNames(parentId));
        }

        return true;
    }

    public bool DeleteSchedule(int parentId, DateTime updateDate)
    {
        using (var scope = scopeProvider.CreateCoreScope(System.Data.IsolationLevel.ReadCommitted))
        {
            try
            {
                var repo = keyValueRepository.Get(KeyValueSchedule + parentId.ToString());
                if (repo == null || updateDate != repo.UpdateDate)
                {
                    return false;
                }
                repo.Value = DeleteValue;
                repo.UpdateDate = DateTime.UtcNow;
                keyValueRepository.Save(repo);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                scope.Complete();
            }
        }
        return true;
    }


    public IEnumerable<Tuple<int, DateTime>> GetSchedules()
    {
        using (var scope = scopeProvider.CreateCoreScope(System.Data.IsolationLevel.ReadCommitted))
        {
            var results = new List<Tuple<int, DateTime>>();
            try
            {
                var schedules = keyValueRepository.FindByKeyPrefix(KeyValueSchedule);
                if (schedules == null)
                {
                    return results;
                }

                foreach (var schedule in schedules)
                {
                    var repo = keyValueRepository.Get(schedule.Key);
                    if (repo != null && repo.Value != DeleteValue)
                    {
                        results.Add(new Tuple<int, DateTime>(int.TryParse(repo.Value, out var v) ? v : 0, repo.UpdateDate));
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GetSchedules() has error");
                return results;
            }
            finally
            {
                scope.Complete();
            }
        }
    }

    private bool Valid([NotNullWhen(true)] IContent? content) => content != null && !content.Trashed && content.Published && content.Id != Constants.System.Root;

    private DateTime CurrentReviewDate(int id)
    {
        var current = contentService.GetById(id);

        while (Valid(current))
        {
            var status = ReviewStatuses.ParseByDescription(current.GetValue<string>(typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewStatus))));
            switch (status)
            {
                case ReviewStatuses.Enable:
                    return current.GetValue<DateTime>(typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewDate)));

                case ReviewStatuses.Disable:
                    return DateTime.MinValue;
            }
            current = contentService.GetById(current.ParentId);
        }

        return DateTime.MinValue;
    }

    private IEnumerable<string> CurrentPathNames(int id)
    {
        string? name = null;
        var names = new List<string>();
        var current = contentService.GetById(id);
        while (Valid(current))
        {
            if (name != null)
            {
                names.Add(name);
            }
            current = contentService.GetById(current.ParentId);
            name = current?.Name;
        }
        return names;
    }

    private Tuple<DateTime, string> UpdateContent(IIndex index, int contentId, DateTime parentReviewDate, IEnumerable<string> pathNames)
    {
        var current = contentService.GetById(contentId);
        var name = current?.Name ?? "Content " + current?.Id ?? string.Empty;
        if (!Valid(current) || current.ContentType.Alias != EditorPage.ModelTypeAlias)
        {
            index.DeleteFromIndex(contentId.ToString());
            return new Tuple<DateTime, string>(DateTime.MinValue, name);
        }

        var status = ReviewStatuses.ParseByDescription(current.GetValue<string>(typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewStatus))));
        var reviewDate = parentReviewDate;
        if (status == ReviewStatuses.Inherit)
        {
            status = parentReviewDate == DateTime.MinValue ? ReviewStatuses.Disable : ReviewStatuses.Enable;
        }
        else if (status == ReviewStatuses.Enable)
        {
            reviewDate = current.GetValue<DateTime>(typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewDate)));
        }
        
        if (status == ReviewStatuses.Enable && reviewDate != DateTime.MinValue)
        {
            index.IndexItems(editorIndexValueSetBuilder.GetValueSets(reviewDate, pathNames, current));
        }
        else
        {
            index.DeleteFromIndex(contentId.ToString());
        }
        return new Tuple<DateTime, string>(reviewDate, name);
    }


    static volatile int ExecuteNowRunning = 0;

    public bool ExectionOfAllContentNow() => ExecuteNow(AllContent);

    public bool ExecuteNow(int parentId)
    {
        logger.LogInformation(nameof(ReviewSearchBackgroundTask) + "." + nameof(ExecuteNow) + "(" + parentId.ToString() + "): Started");
        var myExecuteNowRunning = Interlocked.Increment(ref ExecuteNowRunning);

        if (!examineManager.TryGetIndex(nameof(ReviewIndex), out IIndex? index))
        {
            throw new InvalidOperationException("Could not obtain the " + nameof(ReviewIndex));
        }

        var process = new Stack<Tuple<DateTime, IEnumerable<string>, int>>();
        if (parentId == AllContent)
        {
            foreach (var root in contentService.GetRootContent())
            {
                process.Push(new Tuple<DateTime, IEnumerable<string>, int>(DateTime.MinValue, Enumerable.Empty<string>(), root.Id));
            }
        }
        else
        {
            process.Push(new Tuple<DateTime, IEnumerable<string>, int>(CurrentReviewDate(parentId), CurrentPathNames(parentId), parentId));
        }

        while (process.Any())
        {
            if (ExecuteNowRunning != myExecuteNowRunning)
            {
                // Someone else is trying to run this method, so exit
                logger.LogInformation(nameof(ReviewSearchBackgroundTask) + "." + nameof(ExecuteNow) + "(" + parentId.ToString() + "): Finished, someone else interrupted us");
                return true;
            }

            var item = process.Pop();
            var reviewDate = UpdateContent(index, item.Item3, item.Item1, item.Item2);
            var pathNames = item.Item2.ToList();
            pathNames.Add(reviewDate.Item2);
            foreach (var child in contentService.GetPagedChildren(item.Item3, 0, 1000000, out var _))
            {
                process.Push(new Tuple<DateTime, IEnumerable<string>, int>(reviewDate.Item1, pathNames, child.Id));
            }
            Thread.Sleep(1);
        }
        logger.LogInformation(nameof(ReviewSearchBackgroundTask) + "." + nameof(ExecuteNow) + "(" + parentId.ToString() + "): Finished Sucessfully");
        return true;
    }

}

