using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorMapPanel
{
    public EditorMapTypes MapTypeTyped => EditorMapTypes.ParseByDescription(this.MapType) ?? EditorMapTypes.PublicRightOfWay;
    public EditorNamedIcons PanelIconTyped => EditorNamedIcons.ParseByDescription(this.PanelIcon) ?? EditorNamedIcons.Asterisk;
}
