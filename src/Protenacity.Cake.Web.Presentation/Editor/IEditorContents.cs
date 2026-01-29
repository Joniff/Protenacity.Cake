using Protenacity.Cake.Web.Presentation.Editor.Paging;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor;

public interface IEditorContents : IEditorContent
{
    bool Gap { get; }
    Guid? ParentKey { get; }
    IEnumerable<BlockListItem>? Blocks { get; }
    PagingViewModel? Paging { get; }
}
