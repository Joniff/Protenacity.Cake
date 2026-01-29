using Examine;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Search.Core;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Presentation.Search.BackgroundTask;

public class EditorSearchBackgroundTask(
    ICoreScopeProvider scopeProvider,
    IKeyValueRepository keyValueRepository,
    IContentService contentService,
    IExamineManager examineManager,
    EditorIndexValueSetBuilder editorIndexValueSetBuilder,
    ILogger<EditorSearchBackgroundTask> logger) 
    : IEditorSearchBackgroundTask
{
    private const string KeyValueSchedule = nameof(EditorSearchBackgroundTask) + ":" + nameof(KeyValueSchedule) + ":";
    private const int AllContent = Umbraco.Cms.Core.Constants.System.Root;
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

        if (parentId != AllContent && examineManager.TryGetIndex(nameof(EditorIndex), out IIndex? index))
        {
            UpdateContent(index, parentId, CurrentStatus(parentId));
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
            catch (Exception)
            {
                return results;
            }
            finally
            {
                scope.Complete();
            }
        }
    }

    private bool Valid([NotNullWhen(true)] IContent? content) => content != null && !content.Trashed && content.Published && content.Id != Umbraco.Cms.Core.Constants.System.Root;

    private SeoStatuses CurrentStatus(int id)
    {
        var current = contentService.GetById(id);

        while (Valid(current))
        {
            var status = Web.Core.Extensions.Enum<SeoStatuses>.GetValueByDescription(current.GetValue<string>(typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.SeoStatus))));
            if (status != SeoStatuses.Inherit)
            {
                return status;
            }
            current = contentService.GetById(current.ParentId);
        }

        return SeoStatuses.Disable;
    }

    private SeoStatuses UpdateContent(IIndex index, int contentId, SeoStatuses parentStatus)
    {
        var current = contentService.GetById(contentId);
        if (!Valid(current) || current.ContentType.Alias != EditorPage.ModelTypeAlias)
        {
            index.DeleteFromIndex(contentId.ToString());
            return parentStatus;
        }

        var status = Web.Core.Extensions.Enum<SeoStatuses>.GetValueByDescription(current.GetValue<string>(typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.SeoStatus))));
        if (status == SeoStatuses.Inherit)
        {
            status = parentStatus;
        }
        if (status == SeoStatuses.Disable)
        {
            index.DeleteFromIndex(contentId.ToString());
        }
        else
        {
            index.IndexItems(editorIndexValueSetBuilder.GetValueSets(current));
        }
        return status;
    }

    public bool ExectionOfAllContentNow() => ExecuteNow(AllContent);


    static volatile int ExecuteNowRunning = 0;

    public bool ExecuteNow(int parentId)
    {
        logger.LogInformation(nameof(EditorSearchBackgroundTask) + "." + nameof(ExecuteNow) + "(" + parentId.ToString() + "): Started");

        var myExecuteNowRunning = Interlocked.Increment(ref ExecuteNowRunning);

        if (!examineManager.TryGetIndex(nameof(EditorIndex), out IIndex? index))
        {
            throw new InvalidOperationException("Could not obtain the " + nameof(EditorIndex));
        }

        var process = new Stack<Tuple<SeoStatuses, int>>();
        if (parentId == AllContent)
        {
            foreach (var root in contentService.GetRootContent())
            {
                process.Push(new Tuple<SeoStatuses, int>(SeoStatuses.Enable, root.Id));
            }
        }
        else
        {
            process.Push(new Tuple<SeoStatuses, int>(CurrentStatus(parentId), parentId));
        }

        while (process.Any())
        {
            if (ExecuteNowRunning != myExecuteNowRunning)
            {
                // Someone else is trying to run this method, so exit
                logger.LogInformation(nameof(EditorSearchBackgroundTask) + "." + nameof(ExecuteNow) + "(" + parentId.ToString() + "): Finished, somebody else interrupted us");
                return true;
            }

            var item = process.Pop();
            var status = UpdateContent(index, item.Item2, item.Item1);

            foreach (var child in contentService.GetPagedChildren(item.Item2, 0, 1000000, out var _))
            {
                process.Push(new Tuple<SeoStatuses, int>(status, child.Id));
            }
            Thread.Sleep(1);
        }
        logger.LogInformation(nameof(EditorSearchBackgroundTask) + "." + nameof(ExecuteNow) + "(" + parentId.ToString() + "): Finished successfully");
        return true;
    }

}

