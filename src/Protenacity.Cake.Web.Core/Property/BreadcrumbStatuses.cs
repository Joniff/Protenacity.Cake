using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum BreadcrumbStatuses
{
    [Description("Inherit Breadcrumb Status")]
    Inherit,

    [Description("Show Breadcrumb")]
    Show,

    [Description("Hide Breadcrumb")]
    Hide
}
