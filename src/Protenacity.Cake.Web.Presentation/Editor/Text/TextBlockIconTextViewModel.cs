using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockIconTextViewModel
{
    public EditorNamedIcons Icon { get; init; }
    public required IHtmlEncodedString Text { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades Shade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
    public string? IconColor { get; init; }
    public string? LinkUrl { get; init; }
    public bool HasLinkUrl => !string.IsNullOrWhiteSpace(LinkUrl);
    public ActionTargets? LinkTarget { get; init; }
}
