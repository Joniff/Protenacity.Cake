using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum VirtualAgentStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Enable")]
    Enable,

    [Description("Disable")]
    Disable
}
