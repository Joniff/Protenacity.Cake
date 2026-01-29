using Examine;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Search.Internal;

public interface IEditorSearchInternalService
{
    IDictionary<string, object> Desiccate(IContent content);
    IEnumerable<string> ParseCriteria(string criteria);
    IEditorSearchResults SearchExact(int domainId, int[] boosts, int[] priorites, IEnumerable<string> parsedCriteria, int page, int pageSize, IEditorSearchOptions options);
    IEditorSearchResults SearchFuzzy(int domainId, double fuzzy, int[] priorites, IEnumerable<string> parsedCriteria, int page, int pageSize, IEditorSearchOptions options);
    IEnumerable<IEditorSearchResult> SearchCategories(int domainId, IEnumerable<Guid> categories, IEditorSearchOptions options);
}
