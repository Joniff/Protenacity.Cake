using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.BusRoute;
using Protenacity.Cake.Web.Presentation.Editor.Paging;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.BusRoute;

public class BusRouteViewModel : PagingViewModel
{
    public BlockListModel? Background { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
    public EditorBusRouteTypes BusRouteType { get; init; }
    public string? LinkUrl { get; init; }
    public string? MapUrl { get; init; }
    public BusRouteResponse? Result { get; init; }
    public BusRouteExtentResponse? Extent { get; init; }
    public double Ratio { get; init; }
    public required string BusRouteUrl { get; init; }
    public required string LocalityUrl { get; init; }
    public required string PostcodeUrl { get; init; }
    public required string AddressUrl { get; init; }
    public required string BusStopUrl { get; init; }
    public bool ShowSearch { get; init; }
}
