using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

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

public class AsideStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<AsideStatuses>(dataTypeService)
{

    public override string DataTypeName => "Aside Status";
}
