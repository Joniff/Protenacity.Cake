using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum HeaderMenuPositions
{
    [Description("Inside Header Left Justified")]
    InsideLeft,

    [Description("Inside Header Right Justified")]
    InsideRight,

    [Description("Below Header Left Justified")]
    BelowLeft,

    [Description("Below Header Right Justified")]
    BelowRight
}
