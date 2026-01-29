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
    public EditorTabStripPlacements PlacementTyped => Enum<EditorTabStripPlacements>.GetValueByDescription(this.Placement);
    public EditorTabStripStyles StylesTyped => Enum<EditorTabStripStyles>.GetValueByDescription(this.Style);
}
