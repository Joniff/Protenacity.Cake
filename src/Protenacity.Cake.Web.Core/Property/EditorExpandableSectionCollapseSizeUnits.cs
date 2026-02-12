using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorExpandableSectionCollapseSizeUnits
{
    [Description("Screen Percentage")]
    ScreenPercentage,

    [Description("Section Percentage")]
    SectionPercentage,

    [Description("Fixed Height")]
    FixedHeight
}

public class EditorExpandableSectionCollapseSizeUnitsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorExpandableSectionCollapseSizeUnits>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Expandable Section Collapse Size Unit";
}