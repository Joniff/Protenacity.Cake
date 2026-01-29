using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorPanelName
{
    EditorNamedIcons PanelIconTyped { get; }
}

public partial class EditorPanelName
{
    public EditorNamedIcons PanelIconTyped => Enum<EditorNamedIcons>.GetValueByDescription(this.PanelIcon);
}
