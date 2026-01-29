using Umbraco.Cms.Core.Models;

namespace Protenacity.Web.Review.Search.Internal;

public interface IReviewSearchInternalService
{
    IDictionary<string, object> Desiccate(IContent content, IEnumerable<string> pathNames, DateTime reviewDate);
    public IReviewSearchResults Search(int domainId, int? parent, IEnumerable<int>? userGroups, int page, int pageSize);

}
