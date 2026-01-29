using Protenacity.Cake.Web.Core.Constitution;

namespace Protenacity.Cake.Web.Presentation.Category;

public class CategoryHeading
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required IEnumerable<CategoryName> Names { get; init; }

}
