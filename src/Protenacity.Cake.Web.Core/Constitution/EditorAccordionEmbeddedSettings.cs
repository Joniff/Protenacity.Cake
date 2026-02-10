using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorAccordionEmbeddedSettings
{
    EditorAccordionInitialStates InitialStateTyped { get; }
}

public partial class EditorAccordionEmbeddedSettings
{
    public EditorAccordionInitialStates InitialStateTyped => EditorAccordionInitialStates.ParseByDescription(this.InitialState) ?? EditorAccordionInitialStates.AllCollapsed;
}
