using System.ComponentModel;

namespace Protenacity.Cake.Web.Presentation.Search;

public class EditorSearchResult : IEditorSearchResult
{
    [Description("domainId")]
    public int DomainId { get; init; }

    [Description("contentId")]
    public int ContentId { get; init; }

    [Description("path")]
    public required int[] Path { get; init; }

    [Description("h1,h2,h3,h4,h5,h6")]
    public required string?[] Headers { get; init; }

    [Description("body")]
    public string? Body { get; init; }

    [Description("keywords")]
    public string? Keywords { get; init; }

    [Description("promoted")]
    public string? Promoted { get; init; }

    [Description("priority")]
    public required int Priority { get; init; }

    [Description("categories")]
    public required Guid[] Categories { get; init; }

    [Description("abstract")]
    public string? Abstract { get; init; }
}

