using System.ComponentModel;

namespace Protenacity.Web.Review.Search;

public interface IReviewSearchResult
{
    [Description("key")]
    public Guid Key { get; init; }

    [Description("domainId")]
    int DomainId { get; }

    [Description("contentId")]
    int ContentId { get; }

    [Description("pathIds")]
    int[] PathIds { get; }

    [Description("pathNames")]
    IEnumerable<string> PathNames { get; }

    [Description("name")]
    string Name { get; }

    [Description("reviewDate")]
    DateTime ReviewDate { get; }

    [Description("userGroups")]
    IEnumerable<int> UserGroups { get; }
}

