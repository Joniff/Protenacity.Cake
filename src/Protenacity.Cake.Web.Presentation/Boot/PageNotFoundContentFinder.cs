using Protenacity.Cake.Web.Core.Constitution;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Boot;

public class PageNotFoundContentFinder(IUmbracoContextAccessor umbracoConextAccessor) 
    : IContentLastChanceFinder
{
    public Task<bool> TryFindContent(IPublishedRequestBuilder request)
    {
        if (request.Domain?.ContentId == null)
        {
            return Task.FromResult(false);
        }

        var homePage = umbracoConextAccessor.GetRequiredUmbracoContext().Content.GetById(request.Domain.ContentId);
        if (homePage == null)
        {
            return Task.FromResult(false);
        }

        var currentDomainPage = homePage.AncestorOrSelf<DomainPage>()
            ?? throw new ApplicationException($"{homePage.Name}({request.Domain.ContentId}) page doesn\'t have a {DomainPage.ModelTypeAlias} as an ancestor");

        if (currentDomainPage.ErrorPage == null) 
        {
            return Task.FromResult(false);
        }

        request.SetIs404();
        request.SetPublishedContent(currentDomainPage.ErrorPage);

        return Task.FromResult(true);
    }
}
