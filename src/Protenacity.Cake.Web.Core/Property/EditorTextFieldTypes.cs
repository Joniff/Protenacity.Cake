using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTextFieldTypes
{
    [Description("Day of Week")]
    Week,

    [Description("Day of Month")]
    Day,

    [Description("Month")]
    Month,

    [Description("Year")]
    Year,

    [Description("Search Criteria")]
    Criteria,

    [Description("Page")]
    Page,

    [Description("Page Size")]
    PageSize,

    [Description("Total Pages")]
    TotalPages,

    [Description("Category Heading")]
    CategoryHeading,

    [Description("CategoryHeadingDescription")]
    CategoryHeadingDescription,

    [Description("Category")]
    Category,
}

public class EditorTextFieldTypesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorTextFieldTypes>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Text Field Picker";
}