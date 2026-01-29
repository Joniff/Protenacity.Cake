using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Image;

public class ImageViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(Image);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content.Block == null)
        {
            return Content(string.Empty);
        }

        return View(editorService.Cast<IEditorImageEmbedded, IEditorImageEmbeddedSettings>(content.Block));
    }
}
