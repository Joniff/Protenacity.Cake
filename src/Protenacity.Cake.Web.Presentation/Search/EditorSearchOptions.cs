namespace Protenacity.Cake.Web.Presentation.Search;

public class EditorSearchOptions : IEditorSearchOptions
{
    public string HighlightOpenTag { get; init; } = "<b>";
    public string HighlightCloseTag { get; init; } = "</b>";
    public int HighlightFragmentSizeInCharacters { get; init; } = 100;
    public int HighlightFRagmentsMergeInCharacters { get; } = 10;
    public int HighlightMaximumNumberOfFragments { get; init; } = 5;
    public string HighlightFragmentSeparator { get; init; } = "&hellip;";
}
