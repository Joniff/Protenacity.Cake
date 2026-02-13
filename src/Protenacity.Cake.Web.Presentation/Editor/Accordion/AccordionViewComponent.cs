using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Accordion;

public class AccordionViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(Accordion);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content.Block == null)
        {
            return Content(string.Empty);
        }

        var accordion = editorService.Cast<EditorAccordionPrimary, IEditorAccordionEmbeddedSettings>(content.Block);
        var panels = editorService.Load(null, content.Defaults, ((EditorAccordionPrimary)content.Block.Content).PanelBlocks);
        var initialState = accordion?.Settings?.InitialState ?? EditorAccordionInitialStates.FirstPanelExpanded;

        if (panels?.Contents.Any() != true)
        {
            return Content(string.Empty);
        }

        return View(new AccordionViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            InitialStates = accordion?.Settings?.InitialState ?? EditorAccordionInitialStates.FirstPanelExpanded,
            Panels = panels.Contents.Select((x, i) => new Tuple<bool, IEditorContent>(
                initialState == EditorAccordionInitialStates.AllCollapsed ? false : 
                (initialState == EditorAccordionInitialStates.AllPanelsExpanded ? true : i == 0), x))
        });
    }
}
