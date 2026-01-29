using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Video;

public class VideoViewModel : ResponsiveImageViewModel
{
    public required string Id { get; init; }
    public IHtmlEncodedString? Header { get; init; }
    public IHtmlEncodedString? Copyright { get; init; }

    public enum SourceTypes
    {
        YouTube,
        Vimeo,
        Media
    }

    public SourceTypes SourceType { get; init; }

    public required string SourceCode { get; init; }

    public bool ShowControls { get; init; }

    public EditorVideoRatios Ratio { get; init; }
}
