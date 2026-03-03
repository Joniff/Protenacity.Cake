using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCodeLanguage
{
    [Description("C++")]
    Cpp,

    [Description("C#")]
    CSharp,
}

public class EditorCodeLanguageValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCodeLanguage>(dataTypeService)
{
    public override string DataTypeName => "Editor Code Language";
}
