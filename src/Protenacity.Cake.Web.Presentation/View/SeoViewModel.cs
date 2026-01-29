using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.View;

public class SeoViewModel : ISeoViewModel
{
    public bool Enable { get; init; }

    public IEnumerable<string>? Keywords { get; init; }

    public DateTime DatePublished { get; init; }

    public string? Description { get; init;  }

    public int Priority { get; init; }

    public MediaWithCrops? Thumbnail { get; init; }

    public required string Title { get; init; }
}
