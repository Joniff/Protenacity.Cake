namespace Protenacity.Cake.Web.Presentation.BusRoute;

public interface IBusRouteService
{
    BusRouteResponse? SearchBusRoutes(Uri url, string criteria);
    BusRouteResponse? GetDistricts(Uri url);
    BusRouteResponse? GetBusRoute(Uri url, string routeName);
    BusRouteResponse? GetBusService(Uri url, string service);
    BusRouteResponse? GetBusStops(Uri url, string services);
    BusRouteResponse? GetBusStop(Uri url, string busStop);
    BusRouteResponse? GetPostcode(Uri url, string criteria);
    BusRouteResponse? GetAddress(Uri url, string criteria);
    BusRouteExtentResponse? GetServiceMapExtent(Uri url, string serviceId);
    BusRouteExtentResponse? GetBusStopMapExtent(Uri url, string busStopId);
}
