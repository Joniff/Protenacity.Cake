using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorActionEmbeddedSettings
{
    ActionStyles StyleActionTyped { get; }
    ActionStyleClickAreas StyleActionClickAreaTyped { get; }
    ActionStyleAlignments StyleActionAlignmentTyped { get; }
}

public partial class EditorActionEmbeddedSettings
{
    public ActionStyles StyleActionTyped => Enum<ActionStyles>.GetValueByDescription(this.StyleAction);
    public ActionStyleClickAreas StyleActionClickAreaTyped => Enum<ActionStyleClickAreas>.GetValueByDescription(this.StyleActionClickArea);
    public ActionStyleAlignments StyleActionAlignmentTyped => Enum<ActionStyleAlignments>.GetValueByDescription(this.StyleActionAlignment);
}
