namespace Protenacity.Cake.Web.Presentation.Search;

public interface IEditorSearchResults
{
    IEnumerable<string>? DidYouMean { get; }
    IEnumerable<IEditorSearchResult> Results { get; }
    int Page { get; }
    int PageSize { get; }
    int? TotalResults { get; }
    int? TotalPages { get; }
}
