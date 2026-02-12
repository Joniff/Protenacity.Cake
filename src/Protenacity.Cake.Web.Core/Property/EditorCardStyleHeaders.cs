using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCardStyleHeaders
{
    [Description("Show")]
    Show,

    [Description("Hide")]
    Hide
}

public class EditorCardStyleHeadersValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCardStyleHeaders>(dataTypeService)
{

    public override string DataTypeName => "Editor Card Header Style";
}