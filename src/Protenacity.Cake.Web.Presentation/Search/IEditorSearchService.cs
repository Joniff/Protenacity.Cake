using Protenacity.Cake.Web.Core.Constitution;

namespace Protenacity.Cake.Web.Presentation.Search;

public interface IEditorSearchService
{
    IEditorSearchResults Search(DomainPage domainPage, string searchText, int page, int pageSize);
    IEnumerable<IEditorSearchResult> SearchCategoryHeading(DomainPage domainPage, params Guid[] categoryDataKeys);

}
