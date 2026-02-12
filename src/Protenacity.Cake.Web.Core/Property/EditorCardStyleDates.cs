using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCardStyleDates
{
    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}

public class EditorCardStyleDatesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCardStyleDates>(dataTypeService)
{

    public override string DataTypeName => "Editor Card Date Style";
}