using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum CategoryHeadingDescriptionStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}

public class CategoryHeadingDescriptionStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<CategoryHeadingDescriptionStatuses>(dataTypeService)
{

    public override string DataTypeName => "Global Category Heading Description Status";
}
