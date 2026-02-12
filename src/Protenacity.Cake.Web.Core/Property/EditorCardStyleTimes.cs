using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCardStyleTimes
{
    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}

public class EditorCardStyleTimesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCardStyleTimes>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Card Time Style";
}