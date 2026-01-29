using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorOrders
{
    [Description("Default")]
    Default,

    [Description("Latest First")]
    Latest,

    [Description("Oldest First")]
    Oldest,

    [Description("A to Z")]
    AtoZ,

    [Description("Z to A")]
    ZtoA
}
