using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTabsPrimarySettings
{
    public EditorTabStripPlacements PlacementTyped => Enum<EditorTabStripPlacements>.GetValueByDescription(this.Placement);
    public EditorTabStripStyles StylesTyped => Enum<EditorTabStripStyles>.GetValueByDescription(this.Style);
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
}
