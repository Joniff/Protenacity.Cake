using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTableSourceFileSeparators
{
    [Description("Comma ( , )")]
    Comma,

    [Description("Semicolon ( ; )")]
    Semicolon,

    [Description("Tab ( \\t )")]
    Tab,

    [Description("Pipe ( | )")]
    Pipe
}

public class EditorTableSourceFileSeparatorsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorTableSourceFileSeparators>(dataTypeService)
{

    public override string DataTypeName => "Editor Table Source File Separator";
}