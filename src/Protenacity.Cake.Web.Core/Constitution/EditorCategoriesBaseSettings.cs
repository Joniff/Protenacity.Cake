using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorCategoriesBaseSettings
{
    EditorTabStripPlacements PlacementTyped { get; }
}

public partial class EditorCategoriesBaseSettings
{
    public EditorTabStripPlacements PlacementTyped => Enum<EditorTabStripPlacements>.GetValueByDescription(this.Placement);
}
