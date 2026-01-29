using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class DownloadMediaViewModel
{
    public required string Id { get; init; }
    public required IActionViewModel Action { get; init; }
    public required IHtmlEncodedString Header { get; init; }
    public required IHtmlEncodedString Text { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
