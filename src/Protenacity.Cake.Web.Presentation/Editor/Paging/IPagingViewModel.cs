namespace Protenacity.Cake.Web.Presentation.Editor.Paging;

public interface IPagingViewModel
{
    string QueryString { get; }
    string Id { get; }
    string? Title { get; }
    string? Criteria { get; }
    int? Page { get; }
    int? PageSize { get; }
    int? TotalResults { get; }
    int? TotalPages { get; }
}
