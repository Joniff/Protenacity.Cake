using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTextExpandableInitialStates
{
    [Description("Collapsed")]
    Collapsed,

    [Description("Expanded")]
    Expanded
}

public class EditorTextExpandableInitialStatesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorTextExpandableInitialStates>(dataTypeService)
{

    public override string DataTypeName => "Editor Text Expandable Initial State";
}