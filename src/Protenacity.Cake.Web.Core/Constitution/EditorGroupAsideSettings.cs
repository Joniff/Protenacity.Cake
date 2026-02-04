using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorGroupAsideSettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    public EditorTextRestricts TextRestrictTyped => EditorTextRestricts.ParseByDescription(this.TextRestrict);
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges, EditorBorderEdges.All);
}
