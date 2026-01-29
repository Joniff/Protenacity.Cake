namespace Protenacity.Cake.Web.Presentation.Editor.Table;

public class TableBlocksViewModel : TableViewModelBase
{
    public required IEnumerable<Tuple<int, IEnumerable<IEditorContent>>> Rows { get; init; }
}
