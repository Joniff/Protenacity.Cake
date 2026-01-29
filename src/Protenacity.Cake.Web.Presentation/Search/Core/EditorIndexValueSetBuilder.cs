using Examine;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.Search.Internal;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Examine;

namespace Protenacity.Cake.Web.Presentation.Search.Core;

public class EditorIndexValueSetBuilder(IEditorSearchInternalService editorSearchInternalService) : IValueSetBuilder<IContent>
{
    public IEnumerable<ValueSet> GetValueSets(params IContent[] contents)
    {
        foreach (var content in contents.Where(c => c.ContentType.Alias == EditorPage.ModelTypeAlias))
        {
            yield return new ValueSet(content.Id.ToString(), IndexTypes.Content, content.ContentType.Alias, editorSearchInternalService.Desiccate(content));
        }
    }
}
