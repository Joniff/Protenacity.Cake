using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

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

public class EditorDayOfWeekValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorDayOfWeek>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Day Of Week";
}