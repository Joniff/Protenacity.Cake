using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorBusTimeTableTypes
{
    [Description("List Bus Stops by Service")]
    ListStopsByService,

    [Description("List Services by Bus Stop")]
    ListServicesByStop,

    [Description("List Days by Service at Bus Stop")]
    ListDaysByServiceStop,

    [Description("List Bus Times by Service at Bus Stop on Specific Day")]
    ListTimesByServiceStopDay,

    [Description("List Bus Times for all Bus Stops of a Specific Service on Specific Day")]
    ListTimesByService,
}
