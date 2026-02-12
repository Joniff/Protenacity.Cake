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
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction.ToString()) ?? ActionStyles.Button;
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.ParseByDescription(this.StyleActionClickArea.ToString()) ?? ActionStyleClickAreas.Action;
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.RightAbsolute;
}
