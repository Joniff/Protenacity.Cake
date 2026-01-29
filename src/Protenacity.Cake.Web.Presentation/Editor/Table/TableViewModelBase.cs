using Protenacity.Cake.Web.Presentation.Editor.Paging;

namespace Protenacity.Cake.Web.Presentation.Editor.Table;

public abstract class TableViewModelBase : PagingViewModel
{
    public bool FirstRowIsHeader { get; init; }
    public bool FirstColumnIsHeader { get; init; }
    public string? AlternateBackgroundRows { get; init; }
    public string? AlternateBackgroundColumns { get; init; }
}
