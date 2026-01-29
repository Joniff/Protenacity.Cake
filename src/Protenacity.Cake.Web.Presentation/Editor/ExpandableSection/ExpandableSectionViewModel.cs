using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Presentation.Editor.ExpandableSection;

public class ExpandableSectionViewModel
{
    public required string Id { get; init; }
    public required IEnumerable<IEditorContent> Body { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShades { get; init; }
    public EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnit { get; init; }
    public int CollapsedSize { get; init; }
    public EditorExpandableSectionInitialStates InitialState { get; init; }
    public EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethod { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }

}
