using Examine;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Web.Review.Search.Internal;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Examine;

namespace Protenacity.Web.Review.Search.Core;

public class ReviewIndexValueSetBuilder(IReviewSearchInternalService reviewSearchInternalService) : IValueSetBuilder<IContent>
{
    public IEnumerable<ValueSet> GetValueSets(params IContent[] contents) => GetValueSets(DateTime.MinValue, Enumerable.Empty<string>(), contents);

    public IEnumerable<ValueSet> GetValueSets(DateTime reviewDate, IEnumerable<string> pathNames, params IContent[] contents)
    {
        foreach (var content in contents.Where(c => c.ContentType.Alias == EditorPage.ModelTypeAlias))
        {
            yield return new ValueSet(content.Id.ToString(), IndexTypes.Content, content.ContentType.Alias, reviewSearchInternalService.Desiccate(content, pathNames, reviewDate));
        }
    }
}
