using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

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

public class AlertTypesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<AlertTypes>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Alert Type";
}