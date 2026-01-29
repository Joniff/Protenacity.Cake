using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Section;

public class SectionViewComponent : ViewComponent
{
    public const string Name = nameof(Section);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content == null)
        {
            return Content(string.Empty);
        }

        return View(content);
    }
}
