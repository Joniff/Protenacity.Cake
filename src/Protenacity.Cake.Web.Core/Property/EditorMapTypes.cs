using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorMapTypes
{
    [Description("Standard")]
    Standard = 0,

    [Description("Public Right of Way")]
    PublicRightOfWay = 1,

    [Description("Gritting")]
    Gritting = 2,

    [Description("Regions")]
    Regions = 3,

    [Description("Cycle Routes")]
    CycleRoutes = 4,
}
