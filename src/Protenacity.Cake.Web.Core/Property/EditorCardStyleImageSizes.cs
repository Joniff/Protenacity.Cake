using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCardStyleImageSizes
{
    [Description("Medium")]
    Medium,

    [Description("XX Small")]
    XXSmall,

    [Description("X Small")]
    XSmall,

    [Description("Small")]
    Small,

    [Description("Large")]
    Large,

    [Description("X Large")]
    XLarge,

    [Description("XX Large")]
    XXLarge,

    [Description("XXX Large")]
    XXXLarge,
}

public class EditorCardStyleImageSizesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCardStyleImageSizes>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Card Image Size";
}