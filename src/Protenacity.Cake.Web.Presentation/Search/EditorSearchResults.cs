namespace Protenacity.Cake.Web.Presentation.Search;

internal class EditorSearchResults : IEditorSearchResults
{
    public IEnumerable<string>? DidYouMean { get; init; }
    public required IEnumerable<IEditorSearchResult> Results { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int? TotalResults { get; init; }
    public int? TotalPages => TotalResults != null ? ((TotalResults - 1) / PageSize) + 1 : null;
}
