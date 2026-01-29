using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.Search.Internal;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;

namespace Protenacity.Cake.Web.Presentation.Search;

public class EditorSearchService(IEditorSearchInternalService editorSearchInternalService) : IEditorSearchService
{
    public IEditorSearchResults Search(DomainPage domainPage, string searchText, int page, int pageSize)
    {
        var priorities = domainPage.ConfigSearchSeoPriorities?.Split(',')?.Select(t => int.TryParse(t, out var n) ? n : 1)?.ToArray() ?? new int[0];
        var parsedCriteria = editorSearchInternalService.ParseCriteria(searchText);

        var options = new EditorSearchOptions();

        var results = editorSearchInternalService.SearchExact(domainPage.Id, 
            new int[] {
                domainPage.ConfigSearchH1Boost,
                domainPage.ConfigSearchH2Boost,
                domainPage.ConfigSearchH3Boost,
                domainPage.ConfigSearchH4Boost,
                domainPage.ConfigSearchH5Boost,
                domainPage.ConfigSearchH6Boost,
                domainPage.ConfigSearchKeywordsBoost,
                domainPage.ConfigSearchPromotedSearchTermsBoost
            },
            priorities,
            parsedCriteria,
            page,
            pageSize,
            options);

        if (results.TotalResults < domainPage.ConfigSearchImplementFuzzyResults && domainPage.ConfigSearchFuzziness > 0 && domainPage.ConfigSearchFuzziness < 100)
        {
            var fuzzy = editorSearchInternalService.SearchFuzzy(domainPage.Id, ((double)domainPage.ConfigSearchFuzziness) / 100.0, priorities, parsedCriteria, page, pageSize, options);
            if (fuzzy.Results.Any())
            {
                var merged = results.Results.Union(fuzzy.Results).GroupBy(g => g.ContentId).Select(g => g.First());
                var duplicates = results.Results.Count() + fuzzy.Results.Count() - merged.Count();

                results = new EditorSearchResults
                {
                    DidYouMean = fuzzy.DidYouMean,
                    TotalResults = results.TotalResults + fuzzy.TotalResults - duplicates,
                    Results = merged,
                    Page = page,
                    PageSize = pageSize
                };
            }
        }

        return results;
    }

    public IEnumerable<IEditorSearchResult> SearchCategoryHeading(DomainPage domainPage, params Guid[] categoryDataKeys)
    {
        return editorSearchInternalService.SearchCategories(domainPage.Id, categoryDataKeys, new EditorSearchOptions());
    }
}
