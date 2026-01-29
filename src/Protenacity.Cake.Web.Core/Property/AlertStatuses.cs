using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum AlertStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}
