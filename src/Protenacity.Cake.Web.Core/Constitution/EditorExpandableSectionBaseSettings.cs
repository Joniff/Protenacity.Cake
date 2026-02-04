using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorExpandableSectionBaseSettings
{
    EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnitTyped { get; }
    EditorExpandableSectionInitialStates InitialStateTyped { get; }
    EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethodTyped { get; }
}

public partial class EditorExpandableSectionBaseSettings
{
    public EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnitTyped => EditorExpandableSectionCollapseSizeUnits.ParseByDescription(this.CollapsedSizeUnit);
    public EditorExpandableSectionInitialStates InitialStateTyped => EditorExpandableSectionInitialStates.ParseByDescription(this.InitialState);
    public EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethodTyped => EditorExpandableSectionExpandCollapseMethods.ParseByDescription(this.ExpandCollapseMethod);
}
