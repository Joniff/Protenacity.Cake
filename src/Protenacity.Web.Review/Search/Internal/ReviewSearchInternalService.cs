using Examine;
using Examine.Search;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Web.Review.Search.Core;
using Microsoft.Extensions.Logging;
using System.Text;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Protenacity.Web.Review.Search.Internal;

public class ReviewSearchInternalService(
    IExamineManager examineManager,
    IUserService userService)
    : IReviewSearchInternalService
{
    private static string Key => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.Key));
    private static string DomainId => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.DomainId));
    private static string ContentId => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.ContentId));
    private static string PathIds => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.PathIds));
    private static string PathNames => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.PathNames));
    private static string Name => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.Name));
    private static string ReviewDate => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.ReviewDate));
    private static string UserGroups => typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.UserGroups));

    private static string EditorPage_ReviewStatus => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewStatus));
    private static string EditorPage_ReviewDate => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewDate));
    private static string EditorPage_ReviewGroup => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.ReviewGroup));

    private static int[] UserGroupAll = new int[] { 0 };

    public IDictionary<string, object> Desiccate(IContent content, IEnumerable<string> pathNames, DateTime parentReviewDate)
    {
        var results = new Dictionary<string, object>();

        if (!content.Published)
        {
            return results;
        }

        var path = content.Path.Split(',').Skip(1);
        if (path?.Any() != true)
        {
            return results;
        }
        results.Add(Key, content.Key.ToString("N"));
        results.Add(DomainId, path.First());
        results.Add(ContentId, content.Id);
        results.Add(PathIds, string.Join(' ', path));
        results.Add(PathNames, string.Join(';', pathNames));
        results.Add(Name, content.Name ?? "Content " + content.Id.ToString());

        var reviewDate = content.GetValue<DateTime>(EditorPage_ReviewDate);
        if (reviewDate == DateTime.MinValue)
        {
            reviewDate = parentReviewDate;
        }
        results.Add(ReviewDate, reviewDate);

        var userId = content.GetValue<int>(EditorPage_ReviewGroup);
        var groups = userId != 0 ? userService.GetUserById(userId)?.Groups?.Select(g => g.Id) : UserGroupAll;
        if (groups?.Any() == true)
        {
            results.Add(UserGroups, string.Join(' ', groups));
        }
        return results;
    }

    public IReviewSearchResults Search(int domainId, int? parent, IEnumerable<int>? userGroups, int page, int pageSize)
    {
        if (!examineManager.TryGetIndex(nameof(ReviewIndex), out var index))
        {
            throw new ApplicationException("Unable to find index " + nameof(ReviewIndex));
        }

        var query = new StringBuilder();

        query.Append(DomainId);
        query.Append(':');
        query.Append(domainId);
        if (parent != null)
        {
            query.Append(" AND ");
            query.Append(PathIds);
            query.Append(':');
            query.Append(parent);
        }
        if (userGroups?.Any() == true)
        {
            query.Append(" AND (");
            foreach (var userGroup in userGroups.Union(UserGroupAll).Select((Id, Index) => (Id, Index)))
            {
                if (userGroup.Index != 0)
                {
                    query.Append(" OR ");
                }
                query.Append(UserGroups);
                query.Append(':');
                query.Append(userGroup.Id);
            }
            query.Append(')');
        }

        var results = index.Searcher.CreateQuery().NativeQuery(query.ToString()).OrderBy(new SortableField[] { new SortableField(ReviewDate, SortType.Long) }).Execute(new Examine.Search.QueryOptions(page * pageSize, pageSize));

        return new ReviewSearchResults
        {
            Results = results.Select(r => new ReviewSearchResult
            {
                Key = Guid.Parse(r.Values[Key]),
                ContentId = int.TryParse(r.Id, out var c) ? c : Constants.System.Root,
                DomainId = int.TryParse(r.Values[DomainId], out var d) ? d : domainId,
                PathIds = r.Values[PathIds].Split(' ').Select(p => int.Parse(p)).ToArray(),
                PathNames = r.Values[PathNames].Split(';'),
                Name = r.Values[Name],
                ReviewDate = new DateTime(long.TryParse(r.Values[ReviewDate], out var rd) ? rd : 0L),
                UserGroups = r.Values[UserGroups].Split(' ').Select(p => int.Parse(p)).ToArray(),
            }),
            Page = page,
            PageSize = pageSize,
            TotalResults = (int)results.TotalItemCount
        };
    }
}
