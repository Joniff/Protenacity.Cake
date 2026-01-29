using Protenacity.Cake.Web.Presentation.Coroner;

namespace Protenacity.Cake.Web.Presentation.Editor.Table;

public class TableCoronerViewModel<T> : TableViewModelBase 
    where T : ICoronerInquest
{
    public required string CustomDateFormat { get; init; }
    public required string CustomTimeFormat { get; init; }
    public required IEnumerable<T> Rows { get; init; }
}
