using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorBadgeImageShape
{
    [Description("Circle")]
    Circle,

    [Description("Diamond")]
    Diamond,

    [Description("Hexagon")]
    Hexagon,

    [Description("Square")]
    Square,

    [Description("Star")]
    Star,

    [Description("Triangle")]
    Triangle
}

public class EditorBadgeIconShapeValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorBadgeImageShape>(dataTypeService)
{
    public override string DataTypeName => "Editor Badge Image Shape";
}