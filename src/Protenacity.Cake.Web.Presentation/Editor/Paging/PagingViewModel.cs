namespace Protenacity.Cake.Web.Presentation.Editor.Paging;

public class PagingViewModel : IPagingViewModel
{
    public required string Id { get; init; }
    public string? Title { get; init; }
    public required string QueryString { get; init; }
    public string? Criteria { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public int? TotalResults { get; init; }
    public int? TotalPages => TotalResults != null ? ((TotalResults - 1) / PageSize) + 1 : null;
}
