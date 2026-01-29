using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Accordion;

public class AccordionViewModel
{
    public required string Id { get; init; }
    public EditorAccordionInitialStates InitialStates { get; init; }
    public required IEnumerable<Tuple<bool, IEditorContent>> Panels { get; init; }
}
