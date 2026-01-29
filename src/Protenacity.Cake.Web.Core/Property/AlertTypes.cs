using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum AlertTypes
{
    [Description("Warning")]
    Warning,

    [Description("Primary")]
    Primary,

    [Description("Secondary")]
    Secondary,

    [Description("Success")]
    Success,

    [Description("Info")]
    Info,
}
