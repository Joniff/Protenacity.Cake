using Protenacity.Cake.Web.Presentation.Editor.Paging;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor;

public class EditorContents : EditorContent, IEditorContents
{
    public bool Gap { get; init; }
    public Guid? ParentKey { get; set; }
    public IEnumerable<BlockListItem>? Blocks { get; init; }
    public PagingViewModel? Paging { get; init; }
}
