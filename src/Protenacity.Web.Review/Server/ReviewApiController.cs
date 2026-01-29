using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Web.Review.Search.Internal;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Web.Review.Server;

[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = nameof(Review))]
public class ReviewApiController(IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
    IReviewSearchInternalService reviewSearchInternalService,
    IFusionCache fusionCache,
    IContentService contentService,
    IUserService userService) : ReviewApiControllerBase
{
    public readonly TimeSpan CacheLength = TimeSpan.FromHours(1);

    private class Traverse
    {
        public bool Enabled { get; set; }
        public IEnumerable<int>? UserGroups { get; set; }
        public DateTime? ReviewDate { get; set; }
        public required IEnumerable<string> Path { get; set; }
        public required IPublishedContent Content { get; set; }
    }

    private IEnumerable<int> GetUserGroupIdsFromUser(int userId)
    {
        return fusionCache.GetOrSet<IEnumerable<int>>(nameof(GetUserGroupIdsFromUser) + ":" + userId.ToString(), _ =>
        {
            return (userService.GetUserById(userId)?.Groups?.Select(g => g.Id) ?? Enumerable.Empty<int>());
        }, 
        TimeSpan.FromHours(1));
    }

    [HttpGet("pages")]
    [ProducesResponseType<IEnumerable<ReviewPage>>(StatusCodes.Status200OK)]
    public IEnumerable<ReviewPage> Pages()
    {
        var pages = new Dictionary<int, ReviewPage>();
        var currentUser = backOfficeSecurityAccessor.BackOfficeSecurity?.CurrentUser ?? throw new UnauthorizedAccessException();

        if (currentUser.Groups.Any() == false)
        {
            return pages.Values;
        }

        var allParentIds = new List<int>();

        if (currentUser.StartContentIds?.Any() == true)
        {
            allParentIds.AddRange(currentUser.StartContentIds);
        }
        else
        {
            allParentIds.AddRange(contentService.GetRootContent().Where(r => r.ContentType.Alias == DomainPage.ModelTypeAlias).Select(r => r.Id));
        }

        foreach (var allParentId in allParentIds)
        {
            var content = contentService.GetById(allParentId);
            if (content == null)
            {
                continue; 
            }

            var domainId = content.Id;
            int? contentId = null;
            if (content.ContentType.Alias != DomainPage.ModelTypeAlias)
            {
                var pathIds = content.Path.Split(',').Select(s => int.Parse(s));
                if (pathIds == null || pathIds.Count() < 2)
                {
                    continue;
                }
                domainId = pathIds.ToArray()[2];
                contentId = content.Id;
            }

            var results = reviewSearchInternalService.Search(domainId, contentId, currentUser.Groups.Select(g => g.Id), 0, 100);
            foreach (var result in results.Results) 
            {
                if (!pages.ContainsKey(result.ContentId))
                {
                    pages.Add(result.ContentId, new ReviewPage
                    {
                        Id = result.ContentId,
                        Key = result.Key,
                        DaysLeft = result.ReviewDate.Subtract(DateTime.UtcNow.Date).Days,
                        Name = result.Name,
                        ReviewDate = result.ReviewDate,
                        Url = "/umbraco/section/content/workspace/document/edit/" + result.Key.ToString("d") + "/invariant/tab/contents",
                        Path = result.PathNames
                    });
                }
            }
        }

        return pages.Values.OrderBy(r => r.ReviewDate).ThenBy(r => r.Name);
    }
}
