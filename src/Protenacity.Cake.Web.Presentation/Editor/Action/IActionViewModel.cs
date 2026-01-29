using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Action;

public interface IActionViewModel
{
    ActionStyles Style { get; }
    bool ShowClickArrow { get; }
    ActionStyleAlignments Alignment { get; } 


    string? Url { get; }
    ActionTargets Target { get; }
    IHtmlEncodedString? Name { get; }
    bool Download { get; }
    EditorThemes Theme { get; }
    EditorSubthemes Subtheme { get; }
    EditorThemeShades ThemeShade { get; }
    EditorTextRestricts TextRestrict { get; }
}
