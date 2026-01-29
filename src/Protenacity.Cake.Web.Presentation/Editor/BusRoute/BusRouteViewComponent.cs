using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.BusRoute;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Protenacity.Cake.Web.Presentation.Editor.BusRoute;

public class BusRouteViewComponent(IBusRouteService busRouteService, 
    IViewService viewService) 
    : ThemeViewComponent
{
    public const string Name = nameof(BusRoute);
    public const string QueryString = "qb";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";
    public const string TemplateSearchRoute = "~/Views/Components/" + Name + "/SearchRoute.cshtml";
    public const string TemplateBusRoute = "~/Views/Components/" + Name + "/BusRoute.cshtml";
    public const string TemplateSearchLocation = "~/Views/Components/" + Name + "/SearchLocation.cshtml";
    public const string TemplateBusStops = "~/Views/Components/" + Name + "/BusStops.cshtml";
    public const string TemplateListAreas = "~/Views/Components/" + Name + "/ListAreas.cshtml";

    private BusRouteResponse? GetReponse(EditorBusRouteTypes busRouteType, BusRouteData api, string[]? criteria)
    {
        if (busRouteType == EditorBusRouteTypes.ListAreas)
        {
            return busRouteService.GetDistricts(new Uri(api.EndpointLocality ?? throw new InvalidDataException("Missing Location Endpoint")));
        }

        if (criteria?.Any() != true)
        {
            return null;
        }

        if (busRouteType == EditorBusRouteTypes.SearchRoute)
        {
            return busRouteService.SearchBusRoutes(new Uri(api.EndpointBusRoute ?? throw new InvalidDataException("Missing Bus Route Endpoint")), criteria[0]);
        }

        var route = busRouteService.GetBusRoute(new Uri(api.EndpointBusRoute ?? throw new InvalidDataException("Missing Bus Route Endpoint")), criteria[0]);

        if (busRouteType == EditorBusRouteTypes.BusRoute)
        {
            return route;
        }

        if (busRouteType == EditorBusRouteTypes.BusStops)
        {
            var stop = busRouteService.GetBusStop(new Uri(api.EndpointBusStop ?? throw new InvalidDataException("Missing Bus Stop Endpoint")), criteria[0]);
            if (stop?.Features?.Any() == true)
            {
                return stop;
            }
            if (route != null && criteria.Length > 1)
            {
                stop = busRouteService.GetBusStop(new Uri(api.EndpointBusStop ?? throw new InvalidDataException("Missing Bus Stop Endpoint")), criteria[1]);
                if (stop?.Features?.Any() == true)
                {
                    return stop;
                }
            }
        }
        var all = busRouteService.GetDistricts(new Uri(api.EndpointLocality ?? throw new InvalidDataException("Missing Location Endpoint")));
        if (all == null || all?.Features?.Any() == false)
        {
            return null;
        }

        if (criteria.Length > 0)
        {
            var districts = all?.Features?.Where(f => string.Compare(f.Attribute?.District, criteria[0], StringComparison.OrdinalIgnoreCase) == 0);
            if (districts?.Any() == true)
            {
                return new BusRouteResponse
                {
                    Fields = all!.Fields,
                    Features = districts
                };
            }

            var locality = all?.Features?.Where(f => string.Compare(f.Attribute?.Locality, criteria[0], StringComparison.OrdinalIgnoreCase) == 0);
            if (locality?.Any() == true)
            {
                return new BusRouteResponse
                {
                    Fields = all!.Fields,
                    Features = locality
                };
            }
            var postcodes = busRouteService.GetPostcode(new Uri(api.EndpointPostcode ?? throw new InvalidDataException("Missing Postcode Endpoint")), criteria[0]);
            if (postcodes?.Features?.Any() == true)
            {
                return postcodes;
            }
            var addresses = busRouteService.GetAddress(new Uri(api.EndpointAddress ?? throw new InvalidDataException("Missing Address Endpoint")), criteria[0]);
            if (addresses?.Features?.Any() == true)
            {
                return addresses;
            }
        }
        else
        {
            var locality = all?.Features?.Where(f => string.Compare(f.Attribute?.Locality, criteria[0], StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(f.Attribute?.District, criteria[1], StringComparison.OrdinalIgnoreCase) == 0);
            if (locality?.Any() == true)
            {
                return new BusRouteResponse
                {
                    Fields = all!.Fields,
                    Features = locality
                };
            }
            var postcodes = busRouteService.GetPostcode(new Uri(api.EndpointPostcode ?? throw new InvalidDataException("Missing Postcode Endpoint")), criteria[0]);
            if (postcodes?.Features?.Any() == true)
            {
                return postcodes;
            }
            var addresses = busRouteService.GetAddress(new Uri(api.EndpointAddress ?? throw new InvalidDataException("Missing Address Endpoint")), string.Join(' ', criteria));
            if (addresses?.Features?.Any() == true)
            {
                var address = addresses.Features.Where(f => string.Compare(f.Attribute?.Address1, criteria[0], StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(f.Attribute?.Address2, criteria[1], StringComparison.OrdinalIgnoreCase) == 0);
                return new BusRouteResponse
                {
                    Fields = all!.Fields,
                    Features = address
                };
            }
        }
        return null;

    }

    private BusRouteExtentResponse? GetExtentReponse(EditorBusRouteTypes busRouteType, BusRouteData api, [NotNull] string[] criteria)
    {
        switch (busRouteType)
        {
            case Core.Property.EditorBusRouteTypes.BusRoute:
                return busRouteService.GetServiceMapExtent(new Uri(api.EndpointBusRoute ?? throw new InvalidDataException("Missing Bus Route Endpoint")), criteria[0]);

            case EditorBusRouteTypes.BusStops:
                return busRouteService.GetBusStopMapExtent(new Uri(api.EndpointBusStop ?? throw new InvalidDataException("Missing Bus Stop Endpoint")), criteria.Last());

            default:
                return null;
        }
    }

    private string GetTemplate(EditorBusRouteTypes busRouteType)
    {
        switch (busRouteType)
        {
            case EditorBusRouteTypes.SearchRoute:
                return TemplateSearchRoute;

            case EditorBusRouteTypes.BusRoute:
                return TemplateBusRoute;

            case EditorBusRouteTypes.SearchLocation:
                return TemplateSearchLocation;

            case EditorBusRouteTypes.BusStops:
                return TemplateBusStops;

            case EditorBusRouteTypes.ListAreas:
                return TemplateListAreas;

            default:
                throw new InvalidDataException("Unknown Bus Route Request");
        }
    }

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var busRoute = content.Block?.Content as EditorBusRoutePrimary;
        var busRouteSettings = content.Block?.Settings as EditorBusRoutePrimarySettings;
        var api = busRoute?.BusRouteApi as BusRouteData;
        var page = viewService.CurrentSearchPage >= 0 && viewService.CurrentSearchPage < viewService.CurrentDomainPage.ConfigPageMaximum
            ? viewService.CurrentSearchPage :
            0;
        var pageSize = PageSize(content, viewService.CurrentDomainPage.ConfigPageSizeDefault, viewService.CurrentDomainPage.ConfigPageSizeMaximum);
        if (busRoute == null || api == null || busRouteSettings == null)
        {
            return Content("");
        }

        var criteria = viewService.CurrentSearchCriteria(QueryString)?.Split(new char[] { '|', ',', ';' }).Select(s => s.Trim()).ToArray();
        var result = GetReponse(busRoute.DisplayTypeTyped, api, criteria);
        var extent = criteria == null ? null : GetExtentReponse(busRoute.DisplayTypeTyped, api, criteria);

        return View(GetTemplate(busRoute.DisplayTypeTyped), new BusRouteViewModel
        {
            Id = Guid.NewGuid().ToString("N"),
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content),
            BusRouteType = busRoute.DisplayTypeTyped,
            Criteria = criteria?.Any() == true ? string.Join(',', criteria) : null,
            QueryString = QueryString,
            Page = page,
            Result = result,
            Extent = extent,
            PageSize = pageSize,
            TotalResults = result?.Features?.Count(),
            Title = "Search Bus Routes",
            LinkUrl = busRoute.DetailsPage?.Url,
            MapUrl = busRoute.MapPage?.Url,
            Ratio = busRouteSettings.RatioCalculated,
            BusRouteUrl = api.EndpointBusRoute ?? "",
            LocalityUrl = api.EndpointLocality ?? "",
            PostcodeUrl = api.EndpointPostcode ?? "",
            AddressUrl = api.EndpointAddress ?? "",
            BusStopUrl = api.EndpointBusStop ?? "",
            ShowSearch = busRouteSettings.ShowSearch
        });
    }
}
