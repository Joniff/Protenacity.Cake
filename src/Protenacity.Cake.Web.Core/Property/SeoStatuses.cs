using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum SeoStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Enable")]
    Enable,

    [Description("Disable")]
    Disable
}

public class SeoStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<SeoStatuses>(dataTypeService)
{

    public override string DataTypeName => "Seo Status";
}