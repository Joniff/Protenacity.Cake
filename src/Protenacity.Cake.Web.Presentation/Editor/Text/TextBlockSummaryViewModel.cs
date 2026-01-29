using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockSummaryViewModel
{
    public required string Id { get; init; }
    public IHtmlEncodedString? Header { get; init; }
    public required IHtmlEncodedString Text { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades Shade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
