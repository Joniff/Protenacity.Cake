using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.View;

public interface ISeoViewModel
{
    bool Enable { get; }

    IEnumerable<string>? Keywords { get; }

    DateTime DatePublished { get; }

    string? Description { get;  }

    int Priority { get; }

    MediaWithCrops? Thumbnail { get; }

    string Title { get; }

}
