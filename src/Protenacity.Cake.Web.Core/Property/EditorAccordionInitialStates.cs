using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorAccordionInitialStates
{
    [Description("All Collapsed")]
    AllCollapsed,

    [Description("First Panel Expanded")]
    FirstPanelExpanded,

    [Description("All Panels Expanded")]
    AllPanelsExpanded,
}

public class EditorAccordionInitialStatesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorAccordionInitialStates>(dataTypeService)
{

    public override string DataTypeName => "Editor Accordion Expands";
}