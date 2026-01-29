using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Category;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Category;

public class CategoryViewModel
{
    public required string Id { get; init; }
    public BlockListModel? Background { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
    public EditorTabStripPlacements Placement { get; init; }
    public required IEnumerable<CategoryHeading> Heading { get; init; }
    public bool ShowHeadingDescription { get; init; }
    public bool ShowSeparator { get; init; }
}
