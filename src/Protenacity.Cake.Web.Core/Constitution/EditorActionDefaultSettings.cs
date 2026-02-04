using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorActionDefaultSettings
{
    ActionStyles StyleActionTyped { get; }
    ActionStyleClickAreas StyleActionClickAreaTyped { get; }
    ActionStyleAlignments StyleActionAlignmentTyped { get; }
}

public partial class EditorActionDefaultSettings
{
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction);
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.ParseByDescription(this.StyleActionClickArea);
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.ParseByDescription(this.StyleActionAlignment);
}