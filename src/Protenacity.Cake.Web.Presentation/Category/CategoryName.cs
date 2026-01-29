namespace Protenacity.Cake.Web.Presentation.Category;

public class CategoryName
{
    public required string Name { get; init; }
    public required IEnumerable<Tuple<string, string>> PageNameUrls { get; init; }
}
