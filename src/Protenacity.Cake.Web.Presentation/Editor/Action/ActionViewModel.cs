using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Action;

public class ActionViewModel : IActionViewModel
{
    public ActionStyles Style { get; init; }
    public bool ShowClickArrow { get; init; }
    public ActionStyleAlignments Alignment { get; init; }
    public string? Url { get; init; }
    public ActionTargets Target { get; init; }
    public IHtmlEncodedString? Name { get; init; }
    public bool Download { get; init; }
    public EditorThemes Theme { get; init; } = EditorThemes.Inherit;
    public EditorSubthemes Subtheme { get; init; } = EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShade { get; init; } = EditorThemeShades.Inherit;
    public EditorTextRestricts TextRestrict { get; init; } = EditorTextRestricts.Truncate;
}
