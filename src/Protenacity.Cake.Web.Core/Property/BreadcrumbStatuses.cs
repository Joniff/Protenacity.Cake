using System.ComponentModel;
using Umbraco.Cms.Core.Services;

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

public class BreadcrumbStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<BreadcrumbStatuses>(dataTypeService)
{
    public override string DataTypeName => "Breadcrumb Status";
}