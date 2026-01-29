using System.ComponentModel;

namespace Protenacity.Cake.Web.Presentation.Search;

public interface IEditorSearchResult
{
    [Description("domainId")]
    int DomainId { get; }

    [Description("contentId")]
    int ContentId { get; }

    [Description("path")]
    int[] Path { get; }

    [Description("h1,h2,h3,h4,h5,h6")]
    string?[] Headers { get; }

    [Description("body")]
    string? Body { get; }

    [Description("keywords")]
    string? Keywords { get; }

    [Description("promoted")]
    string? Promoted { get; }

    [Description("priority")]
    int Priority { get; }

    [Description("abstract")]
    string Abstract { get; }
}

