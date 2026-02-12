using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorBackgroundSettings
{
    EditorSubthemes SubthemeTyped { get; }
    EditorThemeShades ThemeShadeTyped { get; }
}

public partial class EditorBackgroundSettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade.ToString()) ?? EditorThemeShades.Inherit;
}
