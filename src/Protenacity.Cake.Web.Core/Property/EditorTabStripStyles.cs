using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTabStripStyles
{
    [Description("Tabs")]
    Tabs,

    [Description("Pills")]
    Pills
}

public class EditorTabStripStylesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorTabStripStyles>(dataTypeService)
{

    public override string DataTypeName => "Editor Tab Strip Style";
}