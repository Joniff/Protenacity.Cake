using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Form;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.FormEntry;

public class FormEntryViewModel : FormReaderEntry
{
    public required string Id { get; init; }
    public BlockListModel? Background { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
