using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum AsideStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Hide")]
    Hide,

    [Description("Left")]
    Left,

    [Description("Right")]
    Right
}
