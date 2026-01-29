using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Script;

public class ScriptViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(Script);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content.Block == null)
        {
            return Content(string.Empty);
        }

        return View(editorService.Cast<IEditorScriptEmbedded, IEditorScriptEmbeddedSettings>(content.Block));
    }
}
