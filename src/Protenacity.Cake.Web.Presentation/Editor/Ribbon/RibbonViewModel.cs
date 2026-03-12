using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Ribbon;

public class RibbonViewModel
{
    public required string Id { get; init; }
    public required IEnumerable<BadgeViewModel> Badges { get; init; }
    public EditorCardStyleImageSizes ImageSize { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
