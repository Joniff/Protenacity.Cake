using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCardStyleImageLocations
{
    [Description("Top")]
    Top,

    [Description("Bottom")]
    Bottom,

    [Description("Left")]
    Left,

    [Description("Responsive Left")]
    ResponsiveLeft,

    [Description("Right")]
    Right,

    [Description("Responsive Right")]
    ResponsiveRight,

    [Description("Behind")]
    Behind,

    [Description("Hide")]
    Hide
}

public class EditorCardStyleImageLocationsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCardStyleImageLocations>(dataTypeService)
{
    public override string DataTypeName => "Editor Card Image Style";
}
