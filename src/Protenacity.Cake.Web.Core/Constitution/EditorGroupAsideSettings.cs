using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorGroupAsideSettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade.ToString()) ?? EditorThemeShades.Inherit  ;
    public EditorTextRestricts TextRestrictTyped => EditorTextRestricts.ParseByDescription(this.TextRestrict.ToString()) ?? EditorTextRestricts.Truncate;
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges.ToString(), EditorBorderEdges.All) ?? EditorBorderEdges.Top;
}
