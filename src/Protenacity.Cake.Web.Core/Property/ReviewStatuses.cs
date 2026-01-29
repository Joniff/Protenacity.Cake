using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum ReviewStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Enable")]
    Enable,

    [Description("Disable")]
    Disable
}
