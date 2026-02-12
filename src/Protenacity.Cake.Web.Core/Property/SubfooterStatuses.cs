using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum SubfooterStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}

public class SubfooterStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<SubfooterStatuses>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Subfooter Status";
}