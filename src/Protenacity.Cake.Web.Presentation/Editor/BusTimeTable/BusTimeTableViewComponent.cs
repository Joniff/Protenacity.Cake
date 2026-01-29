using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.BusRoute;
using Protenacity.Cake.Web.Presentation.BusTimeTable;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Protenacity.Cake.Web.Presentation.Editor.BusTimeTable;

public class BusTimeTableViewComponent(IBusRouteService busRouteService, IBusTimeTableService busTimeTableService,
    IViewService viewService,
    ILogger<BusTimeTableViewComponent> logger)
    : ThemeViewComponent
{
    public const string Name = nameof(BusTimeTable);
    public const string ServiceQueryString = "service";
    public const string DayQueryString = "day";
    public const string StopQueryString = "stop";

    public const string StopsTemplate = "~/Views/Components/" + Name + "/Stops.cshtml";
    public const string ServicesTemplate = "~/Views/Components/" + Name + "/Services.cshtml";
    public const string DaysTemplate = "~/Views/Components/" + Name + "/Days.cshtml";
    public const string StopTimesTemplate = "~/Views/Components/" + Name + "/StopTimes.cshtml";
    public const string ServiceTimesTemplate = "~/Views/Components/" + Name + "/ServiceTimes.cshtml";

    private IDictionary<string, string>? GetBusStopsForRoute(BusRouteData busRouteApi, string route) => busRouteService.GetBusStops(new Uri(busRouteApi.EndpointBusStop!), route)
        ?.Features?.Where(k => k?.Attribute?.StopId != null && k?.Attribute?.Stop != null && k?.Attribute?.Status == "active").ToDictionary(k => k.Attribute!.StopId!, k => k.Attribute!.Stop!);

    private DayOfWeek StringToDayOfWeek(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return DayOfWeek.Monday;
        }

        if (text.StartsWith("sa", StringComparison.InvariantCultureIgnoreCase))
        {
            return DayOfWeek.Saturday;
        }

        if (text.StartsWith("su", StringComparison.InvariantCultureIgnoreCase))
        {
            return DayOfWeek.Sunday;
        }

        return DayOfWeek.Monday;
    }

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var busTimeTable = content.Block?.Content as EditorBusTimeTablePrimary;
        var busTimeTableSettings = content.Block?.Settings as EditorBusTimeTablePrimarySettings;
        var busRouteApi = busTimeTable?.BusRouteApi as BusRouteData;
        var busTimeTableApi = busTimeTable?.BusTimeTableDataApi as BusTimeTableData;
        var page = viewService.CurrentSearchPage >= 0 && viewService.CurrentSearchPage < viewService.CurrentDomainPage.ConfigPageMaximum
            ? viewService.CurrentSearchPage :
            0;
        var pageSize = PageSize(content, viewService.CurrentDomainPage.ConfigPageSizeDefault, viewService.CurrentDomainPage.ConfigPageSizeMaximum);
        if (busTimeTable == null || busRouteApi == null || busTimeTableApi == null || busTimeTableSettings == null)
        {
            return Content("");
        }

        var service = viewService.CurrentSearchCriteria(ServiceQueryString);
        var day = StringToDayOfWeek(viewService.CurrentSearchCriteria(DayQueryString));
        var stop = viewService.CurrentSearchCriteria(StopQueryString);
        var criteria = viewService.CurrentSearchCriteria(BusRoute.BusRouteViewComponent.QueryString)?.Split(new char[] { '|', ',', ';' }).Select(s => s.Trim()).ToArray();

        if (string.IsNullOrWhiteSpace(busRouteApi.EndpointBusStop) || string.IsNullOrWhiteSpace(busRouteApi.EndpointBusRoute))
        {
            logger.LogError("Need to setup API endpoint for Bus Route");
            return Content("");
        }
        if (string.IsNullOrWhiteSpace(busTimeTableApi.EndpointListDaysByServiceAndBusStop) 
            || string.IsNullOrWhiteSpace(busTimeTableApi.EndpointListServicesByStop) 
            || string.IsNullOrWhiteSpace(busTimeTableApi.EndpointListBusTimesByServiceBusStopAndDay))
        {
            logger.LogError("Need to setup API endpoint for Bus Time Table");
            return Content("");
        }

        switch (busTimeTable.DisplayTypeTyped)
        {
            case Core.Property.EditorBusTimeTableTypes.ListStopsByService:
                {
                    if (criteria?.Length > 0 && !string.IsNullOrWhiteSpace(criteria[0]))
                    {
                        service = criteria[0];
                    }

                    if (string.IsNullOrWhiteSpace(service))
                    {
                        return Content("Invalid Service");
                    }

                    var info = busRouteService.GetBusService(new Uri(busRouteApi.EndpointBusRoute), service);
                    var attr = info?.Features?.FirstOrDefault()?.Attribute;

                    if (string.IsNullOrWhiteSpace(attr?.Id))
                    {
                        return Content("Can not find Service");
                    }

                    var busStops = GetBusStopsForRoute(busRouteApi, attr.Id);

                    return View(StopsTemplate, new BusTimeTableViewModel<IDictionary<string, string>>
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Subtheme = Subtheme(content),
                        ThemeShade = ThemeShade(content),
                        OverrideColor = OverrideColor(content),
                        BorderColor = BorderColor(content),
                        BorderEdges = BorderEdges(content),
                        Criteria = service,
                        QueryString = BusRoute.BusRouteViewComponent.QueryString,
                        Page = page,
                        PageSize = pageSize,
                        Result = busStops,
                        ServiceCode = service,
                        BusRouteCode = attr.Id,
                        BusRouteName = attr.Route,
                        LinkUrl = busTimeTable.DetailsPage?.Url,
                        MapUrl = busTimeTable.MapPage?.Url,
                    });
                }

            case Core.Property.EditorBusTimeTableTypes.ListServicesByStop:
                {
                    if (criteria?.Length > 0 && !string.IsNullOrWhiteSpace(criteria[0]))
                    {
                        stop = criteria[0];
                    }

                    if (string.IsNullOrWhiteSpace(stop))
                    {
                        return Content("Invalid Stop");
                    }

                    var services = busTimeTableService.ListServicesByStop(new Uri(busTimeTableApi.EndpointListServicesByStop), stop);

                    return View(ServicesTemplate, new BusTimeTableViewModel<IDictionary<string, string>>
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Subtheme = Subtheme(content),
                        ThemeShade = ThemeShade(content),
                        OverrideColor = OverrideColor(content),
                        BorderColor = BorderColor(content),
                        BorderEdges = BorderEdges(content),
                        Criteria = service,
                        QueryString = BusRoute.BusRouteViewComponent.QueryString,
                        Page = page,
                        PageSize = pageSize,
                        Result = services,
                        Services = services,
                        StopCode = stop,
                        LinkUrl = busTimeTable.DetailsPage?.Url,
                        MapUrl = busTimeTable.MapPage?.Url,
                    });
                }

            case Core.Property.EditorBusTimeTableTypes.ListDaysByServiceStop:
                {
                    if (criteria?.Length > 1 && !string.IsNullOrWhiteSpace(criteria[0]) && !string.IsNullOrWhiteSpace(criteria[1]))
                    {
                        stop = criteria[1];
                        service = criteria[0];
                    }

                    if (string.IsNullOrWhiteSpace(stop))
                    {
                        return Content("Invalid Stop");
                    }

                    if (string.IsNullOrWhiteSpace(service))
                    {
                        return Content("Invalid Service");
                    }

                    var info = busRouteService.GetBusService(new Uri(busRouteApi.EndpointBusRoute), service);
                    var attr = info?.Features?.FirstOrDefault()?.Attribute;

                    if (string.IsNullOrWhiteSpace(attr?.Id))
                    {
                        return Content("Can not find Service");
                    }

                    return View(DaysTemplate, new BusTimeTableViewModel<IEnumerable<DayOfWeek>>
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Subtheme = Subtheme(content),
                        ThemeShade = ThemeShade(content),
                        OverrideColor = OverrideColor(content),
                        BorderColor = BorderColor(content),
                        BorderEdges = BorderEdges(content),
                        Criteria = service + "," + stop,
                        QueryString = BusRoute.BusRouteViewComponent.QueryString,
                        Page = page,
                        PageSize = pageSize,
                        Result = busTimeTableService.ListDaysByServiceStop(new Uri(busTimeTableApi.EndpointListDaysByServiceAndBusStop), service, stop),
                        Services = busTimeTableService.ListServicesByStop(new Uri(busTimeTableApi.EndpointListServicesByStop), stop),
                        ServiceCode = service,
                        StopCode = stop,
                        BusRouteCode = attr.Id,
                        BusRouteName = attr.Route,
                        LinkUrl = busTimeTable.DetailsPage?.Url,
                        MapUrl = busTimeTable.MapPage?.Url,
                    });
                }
            case Core.Property.EditorBusTimeTableTypes.ListTimesByServiceStopDay:
                {
                    if (criteria?.Length > 0 && !string.IsNullOrWhiteSpace(criteria[0]))
                    {
                        service = criteria[0];
                        day = DayOfWeek.Monday;

                        if (criteria?.Length > 1 && !string.IsNullOrWhiteSpace(criteria[1]))
                        {
                            stop = criteria[1];

                            if (criteria?.Length > 2 && !string.IsNullOrWhiteSpace(criteria[2]))
                            {
                                day = StringToDayOfWeek(criteria[2]);
                            }
                        }
                    }

                    if (string.IsNullOrWhiteSpace(service))
                    {
                        return Content("Invalid Service");
                    }

                    var results = new Dictionary<DayOfWeek, IDictionary<DateTime, string>>();

                    if (!string.IsNullOrWhiteSpace(stop))
                    {
                        var maxDays = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                        foreach (var maxDay in maxDays)
                        {
                            var times = busTimeTableService.ListTimeTableByServiceStopDay(new Uri(busTimeTableApi.EndpointListBusTimesByServiceBusStopAndDay), service, stop, maxDay);
                            if (times.Any())
                            {
                                results.Add(maxDay, times);
                            }
                        }
                    }

                    var info = busRouteService.GetBusService(new Uri(busRouteApi.EndpointBusRoute), service);
                    var attr = info?.Features?.FirstOrDefault()?.Attribute;
                    if (string.IsNullOrWhiteSpace(attr?.Id))
                    {
                        return Content("Can not find Service");
                    }

                    return View(StopTimesTemplate, new BusTimeTableViewModel<IDictionary<DayOfWeek, IDictionary<DateTime, string>>>
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Subtheme = Subtheme(content),
                        ThemeShade = ThemeShade(content),
                        OverrideColor = OverrideColor(content),
                        BorderColor = BorderColor(content),
                        BorderEdges = BorderEdges(content),
                        Criteria = stop + "," + service + "," + day,
                        QueryString = BusRoute.BusRouteViewComponent.QueryString,
                        Page = page,
                        PageSize = pageSize,
                        Result = results,
                        Services = stop != null ? busTimeTableService.ListServicesByStop(new Uri(busTimeTableApi.EndpointListServicesByStop), stop) : null,
                        Stops = GetBusStopsForRoute(busRouteApi, attr.Id),
                        ServiceCode = service,
                        StopCode = stop,
                        Day = day,
                        BusRouteCode = attr.Id,
                        BusRouteName = attr.Route,
                        LinkUrl = busTimeTable.DetailsPage?.Url,
                        MapUrl = busTimeTable.MapPage?.Url,
                    });
                }

            case Core.Property.EditorBusTimeTableTypes.ListTimesByService:
                {
                    if (criteria?.Length > 0 && !string.IsNullOrWhiteSpace(criteria[0]))
                    {
                        service = criteria[0];
                        day = DayOfWeek.Monday;
                        if (criteria?.Length > 2 && !string.IsNullOrWhiteSpace(criteria[2]))
                        {
                            day = StringToDayOfWeek(criteria[2]);
                        }
                        else if (criteria?.Length > 1 && !string.IsNullOrWhiteSpace(criteria[1]))
                        {
                            day = StringToDayOfWeek(criteria[1]);
                        }
                    }

                    if (string.IsNullOrWhiteSpace(service))
                    {
                        return Content("Invalid Service");
                    }

                    var info = busRouteService.GetBusService(new Uri(busRouteApi.EndpointBusRoute), service);
                    var attr = info?.Features?.FirstOrDefault()?.Attribute;
                    if (string.IsNullOrWhiteSpace(attr?.Id))
                    {
                        return Content("Can not find Service");
                    }

                    var busStops = GetBusStopsForRoute(busRouteApi, attr.Id);
                    if (busStops?.Any() != true)
                    {
                        return Content("Can not find any bus stops for bus service");
                    }

                    var maxDays = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                    var results = new Dictionary<DayOfWeek, IDictionary<KeyValuePair<string, string>, IDictionary<DateTime, string>>>();
                    foreach (var maxDay in maxDays)
                    {
                        var subResults = new Dictionary<KeyValuePair<string, string>, IDictionary<DateTime, string>>();
                        foreach (var busStop in busStops)
                        {
                            var times = busTimeTableService.ListTimeTableByServiceStopDay(new Uri(busTimeTableApi.EndpointListBusTimesByServiceBusStopAndDay), service, busStop.Key, maxDay);
                            if (times.Any())
                            {
                                subResults.Add(busStop, times);
                            }
                        }
                        results.Add(maxDay, subResults);
                    }

                    return View(ServiceTimesTemplate, new BusTimeTableViewModel<IDictionary<DayOfWeek, IDictionary<KeyValuePair<string, string>, IDictionary<DateTime, string>>>>
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Subtheme = Subtheme(content),
                        ThemeShade = ThemeShade(content),
                        OverrideColor = OverrideColor(content),
                        BorderColor = BorderColor(content),
                        BorderEdges = BorderEdges(content),
                        Criteria = stop + "," + service + "," + day,
                        QueryString = BusRoute.BusRouteViewComponent.QueryString,
                        Page = page,
                        PageSize = pageSize,
                        Result = results,
                        Services = busTimeTableService.ListServicesByStop(new Uri(busTimeTableApi.EndpointListServicesByStop), stop),
                        ServiceCode = service,
                        StopCode = stop,
                        Day = day,
                        BusRouteCode = attr.Id,
                        BusRouteName = attr.Route,
                        LinkUrl = busTimeTable.DetailsPage?.Url,
                        MapUrl = busTimeTable.MapPage?.Url,
                    });
                }

            default:
                logger.LogError("Bus Time Table Block has wrong Display TYpe");
                return Content("Invalid Display Type");

        }
    }
}
