using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

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

public class AlertStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<AlertStatuses>(dataTypeService)
{
    public override string DataTypeName => "Alert Status";
}
