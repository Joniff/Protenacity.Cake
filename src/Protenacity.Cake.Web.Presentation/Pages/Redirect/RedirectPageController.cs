using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Protenacity.Cake.Web.Presentation.Pages.Redirect;

public class RedirectPageController(
    ILogger<RedirectPageController> logger,
    IViewService viewService,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor) 
    : RenderController(logger, compositeViewEngine, umbracoContextAccessor)
{
    public override IActionResult Index()
    {
        var model = CurrentPage as RedirectPage 
            ?? throw new ApplicationException("This controller is for " + nameof(RedirectPage) + " only");

        var url = model.Redirect;

        if (string.IsNullOrWhiteSpace(url?.Url))
        {
            logger.LogError($"{model.Name} ({model.Id}/{model.Key})doesn\'t contain a redirect");

            return Redirect("/");
        }

        viewService.OriginalContent = CurrentPage;

        return Redirect(url.Url);
    }
}
