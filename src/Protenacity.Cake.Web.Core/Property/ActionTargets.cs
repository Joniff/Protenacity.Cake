using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionTargets
{
    [Description("_self")]
    CurrentTab,

    [Description("_blank")]
    NewTab
}
