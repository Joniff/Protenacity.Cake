using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorThemeShades
{
    [Description("Inherit")]
    Inherit,

    [Description("Light")]
    Light,

    [Description("Dark")]
    Dark
}

public class EditorThemeShadesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorThemeShades>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.RadioButtonList;
    public override string DataTypeName => "Editor Theme Shade Picker";
}