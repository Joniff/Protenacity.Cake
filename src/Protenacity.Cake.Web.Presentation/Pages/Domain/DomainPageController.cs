using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Pages.Domain;

public class DomainPageController(
    ILogger<DomainPageController> logger,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor) 
    : RenderController(logger, compositeViewEngine, umbracoContextAccessor)
{
    public override IActionResult Index()
    {
        var home = CurrentPage?.ChildrenOfType(EditorPage.ModelTypeAlias)?.FirstOrDefault(c => c.IsVisible() == true);
        if (home == null)
        {
            return BadRequest();
        }

        // Need to set domain on Home page not on this node
        return BadRequest();
    }
}

