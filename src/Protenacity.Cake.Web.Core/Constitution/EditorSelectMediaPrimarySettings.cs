using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorSelectMediaPrimarySettings
{
    public EditorOrders OrderTyped => EditorOrders.ParseByDescription(this.Order.ToString()) ?? EditorOrders.Default;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme.ToString()) ?? EditorSubthemes.Primary;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade.ToString()) ?? EditorThemeShades.Light;
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges.ToString(), EditorBorderEdges.All) ?? EditorBorderEdges.Top;
}
