using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorListPanel
{
    public EditorNamedIcons PanelIconTyped => Enum<EditorNamedIcons>.GetValueByDescription(this.PanelIcon);
}
