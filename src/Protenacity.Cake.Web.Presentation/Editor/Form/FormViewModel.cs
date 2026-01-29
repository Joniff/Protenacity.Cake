namespace Protenacity.Cake.Web.Presentation.Editor.Form;

public class FormViewModel
{
    public required string Id { get; init; }
    public required Guid FormId { get; init; }
    public required string FormTheme { get; init; }
}
