using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorSelectMediaPrimarySettings
{
    public EditorOrders OrderTyped => EditorOrders.ParseByDescription(this.Order);
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges, EditorBorderEdges.All);
}
