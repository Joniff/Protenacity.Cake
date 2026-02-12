using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCardStyleTexts
{
    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}

public class EditorCardStyleTextsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCardStyleTexts>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Card Text Style";
}