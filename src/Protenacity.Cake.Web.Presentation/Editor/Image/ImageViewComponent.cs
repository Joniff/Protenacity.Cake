using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.Image;

public class ImageViewComponent : ViewComponent
{
    public const string Name = nameof(Image);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent<MediaWithCrops> content)
    {
        if (content.ExtraData == null)
        {
            return Content(string.Empty);
        }

        return View(new ImageViewModel
        {
            Image = content.ExtraData,
            FullScreen = (content.Block?.Settings as EditorImageEmbeddedSettings)?.Fullscreen ?? false
        });
    }
}
