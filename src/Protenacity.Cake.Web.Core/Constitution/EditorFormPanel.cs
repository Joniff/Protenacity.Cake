using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorFormPanel
{
    public EditorNamedIcons PanelIconTyped => EditorNamedIcons.ParseByDescription(this.PanelIcon);
}
