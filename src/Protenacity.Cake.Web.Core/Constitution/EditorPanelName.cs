using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorPanelName
{
    EditorNamedIcons PanelIconTyped { get; }
}

public partial class EditorPanelName
{
    public EditorNamedIcons PanelIconTyped => EditorNamedIcons.ParseByDescription(this.PanelIcon) ?? EditorNamedIcons.Asterisk;
}
