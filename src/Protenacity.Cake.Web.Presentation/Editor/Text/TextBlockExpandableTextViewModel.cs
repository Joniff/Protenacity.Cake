using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockExpandableTextViewModel
{
    public required string Id { get; init; }
    public required IHtmlEncodedString Header { get; init; }
    public required IHtmlEncodedString Text { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades Shade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? IconColor { get; init; }
    public EditorTextExpandableInitialStates InitialState { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
