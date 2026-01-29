using System.Collections.ObjectModel;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Protenacity.Cake.Web.Presentation.Editor;

public interface IEditorService
{
    BlockListItem<TContent, TSettings> Cast<TContent, TSettings>(BlockListItem block)
        where TContent : IPublishedElement
        where TSettings : IPublishedElement;

    IEditorContentPaging? Load(Guid? parentContentKey, EditorDefaults? defaults, ReadOnlyCollection<BlockListItem>? sources);
    IEditorContentPaging? Load(Guid? parentContentKey, EditorDefaults? defaults, IEnumerable<BlockListItem>? sources);
}
