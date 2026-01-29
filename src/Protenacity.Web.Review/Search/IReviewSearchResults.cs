namespace Protenacity.Web.Review.Search;

public interface IReviewSearchResults
{
    IEnumerable<IReviewSearchResult> Results { get; }
    int Page { get; }
    int PageSize { get; }
    int? TotalResults { get; }
    int? TotalPages { get; }
}
