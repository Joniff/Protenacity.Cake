using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTabsPrimarySettings
{
    public EditorTabStripPlacements PlacementTyped => EditorTabStripPlacements.ParseByDescription(this.Placement.ToString()) ?? EditorTabStripPlacements.Top;
    public EditorTabStripStyles StylesTyped => EditorTabStripStyles.ParseByDescription(this.Style.ToString()) ?? EditorTabStripStyles.Tabs;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade) ?? EditorThemeShades.Inherit;
}
