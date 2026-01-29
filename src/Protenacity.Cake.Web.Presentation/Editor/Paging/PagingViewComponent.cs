using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Paging;

public class PagingViewComponent : ViewComponent
{
    public const string Name = nameof(Paging);
    public const string TemplatePaging = "~/Views/Components/" + Name + "/Paging.cshtml";
    public const string TemplateSearchBox = "~/Views/Components/" + Name + "/SearchBox.cshtml";

    public IViewComponentResult Invoke(PageRenders render, IPagingViewModel model)
    {
        return View(render == PageRenders.Paging ? TemplatePaging : TemplateSearchBox,  model);
    }
}
