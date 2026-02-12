using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorSearchResultOutputs
{
    [Description("Highlight")]
    Highlight,

    [Description("Abstract")]
    Abstract
}

public class EditorSearchResultOutputsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorSearchResultOutputs>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Search Result Output";
}