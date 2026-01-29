using Protenacity.Cake.Web.Presentation.Editor.Paging;

namespace Protenacity.Cake.Web.Presentation.Editor;

public class EditorContentPaging : IEditorContentPaging
{
    public required IEnumerable<IEditorContent> Contents { get; init; }
    public IPagingViewModel? Paging { get; init; }
}
