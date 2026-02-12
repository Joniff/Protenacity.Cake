using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTextRestricts
{
    [Description("Truncate")]
    Truncate,

    [Description("Word Wrap")]
    WordWrap
}

public class EditorTextRestrictsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorTextRestricts>(dataTypeService)
{

    public override string DataTypeName => "Editor Text Restrict";
}