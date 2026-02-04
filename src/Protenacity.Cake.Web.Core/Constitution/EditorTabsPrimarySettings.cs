using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTabsPrimarySettings
{
    public EditorTabStripPlacements PlacementTyped => EditorTabStripPlacements.ParseByDescription(this.Placement);
    public EditorTabStripStyles StylesTyped => EditorTabStripStyles.ParseByDescription(this.Style);
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
}
