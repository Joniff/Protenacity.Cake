using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorExpandableSectionExpandCollapseMethods
{
    [Description("Arrow")]
    Arrow,

    [Description("Button")]
    Button,
}

public class EditorExpandableSectionExpandCollapseMethodsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorExpandableSectionExpandCollapseMethods>(dataTypeService)
{

    public override string DataTypeName => "Editor Expandable Section Expand Collapse Method";
}