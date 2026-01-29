namespace Protenacity.Cake.Web.Presentation.Editor.Table;

public class TableSimpleViewModel : TableViewModelBase
{
    public required IEnumerable<Tuple<int, string[]>> Cells { get; init; }
}
