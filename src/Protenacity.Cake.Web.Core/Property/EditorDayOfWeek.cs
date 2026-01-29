using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorDayOfWeek
{
    [Description("Monday")]
    Monday = DayOfWeek.Monday,

    [Description("Tuesday")]
    Tuesday = DayOfWeek.Tuesday,

    [Description("Wednesday")]
    Wednesday = DayOfWeek.Wednesday,

    [Description("Thursday")]
    Thursday = DayOfWeek.Thursday,

    [Description("Friday")]
    Friday = DayOfWeek.Friday,

    [Description("Saturday")]
    Saturday = DayOfWeek.Saturday,

    [Description("Sunday")]
    Sunday = DayOfWeek.Sunday
}
