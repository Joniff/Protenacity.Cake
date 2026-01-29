using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Paging;
using Protenacity.Cake.Web.Presentation.Search;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Search;

public class SearchViewModel : PagingViewModel
{
    public IEnumerable<string>? DidYouMean { get; init; }

    public class Result : EditorSearchResult
    {
        public required string Url { get; init; }
    }

    public required IEnumerable<Result>? Results { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
