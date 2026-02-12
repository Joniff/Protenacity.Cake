using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorSubthemes
{
    [Description("Inherit")]
    Inherit,

    [Description("Primary")]
    Primary,

    [Description("Secondary")]
    Secondary,

    [Description("Tertiary")]
    Tertiary
}

public class EditorSubthemesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorSubthemes>(dataTypeService)
{

    public override string DataTypeName => "Editor Subtheme Picker";
}