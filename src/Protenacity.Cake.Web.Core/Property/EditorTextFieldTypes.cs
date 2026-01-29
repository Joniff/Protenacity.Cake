using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTextFieldTypes
{
    [Description("Day of Week")]
    Week,

    [Description("Day of Month")]
    Day,

    [Description("Month")]
    Month,

    [Description("Year")]
    Year,

    [Description("Search Criteria")]
    Criteria,

    [Description("Page")]
    Page,

    [Description("Page Size")]
    PageSize,

    [Description("Total Pages")]
    TotalPages,

    [Description("Category Heading")]
    CategoryHeading,

    [Description("CategoryHeadingDescription")]
    CategoryHeadingDescription,

    [Description("Category")]
    Category,

    [Description("Bus Route")]
    BusRoute,

    [Description("Bus Route Description")]
    BusRouteDescription,

    [Description("Bus Stop")]
    BusStop,

    [Description("Bus Day")]
    BusDay
}
