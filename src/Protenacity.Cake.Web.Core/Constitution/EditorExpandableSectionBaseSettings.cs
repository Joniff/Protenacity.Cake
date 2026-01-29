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
    public EditorExpandableSectionCollapseSizeUnits CollapsedSizeUnitTyped => Enum<EditorExpandableSectionCollapseSizeUnits>.GetValueByDescription(this.CollapsedSizeUnit);
    public EditorExpandableSectionInitialStates InitialStateTyped => Enum<EditorExpandableSectionInitialStates>.GetValueByDescription(this.InitialState);
    public EditorExpandableSectionExpandCollapseMethods ExpandCollapseMethodTyped => Enum<EditorExpandableSectionExpandCollapseMethods>.GetValueByDescription(this.ExpandCollapseMethod);
}
