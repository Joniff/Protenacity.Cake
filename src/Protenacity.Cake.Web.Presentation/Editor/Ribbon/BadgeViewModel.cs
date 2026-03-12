using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Ribbon;

public class BadgeViewModel : ResponsiveImageViewModel
{
    public IHtmlEncodedString? Header { get; init; }
    public IHtmlEncodedString? Text { get; init; }
    public string? Url { get; init; }
    public ActionTargets Target { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
    public EditorBadgeImageShape Shape { get; init; }
    public bool Color { get; init; }
}
