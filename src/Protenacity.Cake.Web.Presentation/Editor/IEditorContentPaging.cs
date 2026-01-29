using Protenacity.Cake.Web.Presentation.Editor.Paging;

namespace Protenacity.Cake.Web.Presentation.Editor;

public interface IEditorContentPaging
{
    IEnumerable<IEditorContent> Contents { get; init; }
    IPagingViewModel? Paging { get; init; }
}
