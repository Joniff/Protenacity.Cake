namespace Protenacity.Cake.Web.Presentation.Search;

public interface IEditorSearchOptions
{
    string HighlightOpenTag { get; }
    string HighlightCloseTag { get; }
    int HighlightFragmentSizeInCharacters { get; }
    int HighlightFRagmentsMergeInCharacters { get; }
    int HighlightMaximumNumberOfFragments { get; }
    string HighlightFragmentSeparator { get; }
}
