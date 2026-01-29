using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorBusTimeTablePrimarySettings
{
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
    public EditorMapRatios RatioTyped => Enum<EditorMapRatios>.GetValueByDescription(this.Ratio);
    public EditorBorderEdges BorderEdgesTyped => Enum<EditorBorderEdges>.GetValueByDescription(this.BorderEdges, EditorBorderEdges.All);
}
