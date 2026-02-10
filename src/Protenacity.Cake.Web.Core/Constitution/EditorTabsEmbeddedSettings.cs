using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorTabsEmbeddedSettings
{
    EditorTabStripPlacements PlacementTyped { get; }
    EditorTabStripStyles StylesTyped { get; }
}

public partial class EditorTabsEmbeddedSettings
{
    public EditorTabStripPlacements PlacementTyped => EditorTabStripPlacements.ParseByDescription(this.Placement) ?? EditorTabStripPlacements.Top;
    public EditorTabStripStyles StylesTyped => EditorTabStripStyles.ParseByDescription(this.Style) ?? EditorTabStripStyles.Tabs;
}
