using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Tabs;

public class TabsViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(Tabs);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content.Block == null)
        {
            return Content(string.Empty);
        }

        var panels = editorService.Load(null, content.Defaults, (content.Block.Content as EditorTabsPrimary)?.PanelBlocks);

        if (panels?.Contents.Any() != true)
        {
            // We have no panels to show
            return Content(string.Empty);
        }

        return View(new TabsViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Tabs = editorService.Cast<EditorTabsPrimary, EditorTabsPrimarySettings>(content.Block),
            Panels = panels.Contents
        });
    }
}
