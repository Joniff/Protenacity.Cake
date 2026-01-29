using System.ComponentModel;

namespace Protenacity.Web.Review.Search;

public class ReviewSearchResult : IReviewSearchResult
{
    [Description("key")]
    public Guid Key { get; init; }

    [Description("domainId")]
    public int DomainId { get; init; }

    [Description("contentId")]
    public int ContentId { get; init; }

    [Description("pathIds")]
    public required int[] PathIds { get; init; }

    [Description("pathNames")]
    public required IEnumerable<string> PathNames { get; init; }

    [Description("name")]
    public required string Name { get; init; }

    [Description("reviewDate")]
    public DateTime ReviewDate { get; init; }

    [Description("userGroups")]
    public required IEnumerable<int> UserGroups { get; init; }
}
