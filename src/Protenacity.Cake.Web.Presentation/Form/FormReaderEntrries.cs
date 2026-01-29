namespace Protenacity.Cake.Web.Presentation.Form;

public class FormReaderEntries
{
    public required IEnumerable<FormReaderEntry> Entries { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalEntries { get; init; }
}
