using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.SectionLinks;

public class SectionLinksViewModel
{
    public required IEnumerable<ActionViewModel> Actions { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
