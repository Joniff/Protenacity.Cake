using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorExpandableSectionInitialStates
{
    [Description("Collapsed")]
    Collapsed,

    [Description("Expanded")]
    Expanded
}

public class EditorExpandableSectionInitialStatesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorExpandableSectionInitialStates>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Expandable Section Initial State";
}