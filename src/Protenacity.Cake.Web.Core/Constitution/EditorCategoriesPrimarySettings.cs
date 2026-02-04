using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorCategoriesPrimarySettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    public EditorTabStripPlacements PlacementTyped => EditorTabStripPlacements.ParseByDescription(this.Placement);
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges, EditorBorderEdges.All);
}
