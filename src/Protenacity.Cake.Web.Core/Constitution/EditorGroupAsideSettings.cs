using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorGroupAsideSettings
{
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
    public EditorTextRestricts TextRestrictTyped => Enum<EditorTextRestricts>.GetValueByDescription(this.TextRestrict);
    public EditorBorderEdges BorderEdgesTyped => Enum<EditorBorderEdges>.GetValueByDescription(this.BorderEdges, EditorBorderEdges.All);
}
