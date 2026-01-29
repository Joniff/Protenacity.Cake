using Protenacity.Cake.Web.Core.Constitution;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Protenacity.Cake.Web.Presentation.Category;

public interface ICategoryService
{
    IEnumerable<CategoryHeading> GetCategories(DomainPage domainPage, IEnumerable<IPublishedContent> sources);
}

