using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Paging;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.BusTimeTable;

public class BusTimeTableViewModel<T> : PagingViewModel
{
    public BlockListModel? Background { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
    public string? LinkUrl { get; init; }
    public string? MapUrl { get; init; }
    public T? Result { get; init; }
    public IHtmlEncodedString? Introduction { get; init; }
    public string? BusRouteCode { get; init; }
    public string? BusRouteName { get; init; }
    public string? ServiceCode { get; set; }
    public string? StopCode { get; set; }
    public string? StopName { get; set; }
    public DayOfWeek? Day { get; set; }
    public IDictionary<string, string>? Services { get; init; }
    public IDictionary<string, string>? Stops { get; init; }
}
