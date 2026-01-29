using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Paging;

namespace Protenacity.Cake.Web.Presentation.Editor.List;

public class ListViewModel
{
    public required string Id { get; init; }
    public ListTypes ListType { get; init; }
    public required IEnumerable<IEditorContent> Blocks { get; init; }
    public int MaxColumns { get; init; }
    public IPagingViewModel? Paging { get; init; }
}
