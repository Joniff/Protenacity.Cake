using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorBusRouteTypes
{
    [Description("List Areas")]
    ListAreas,

    [Description("Search for Bus Routes by Route")]
    SearchRoute,

    [Description("Search for Bus Stops by Location")]
    SearchLocation,

    [Description("Map of Bus Route")]
    BusRoute,

    [Description("Map of Bus Stops")]
    BusStops
}
