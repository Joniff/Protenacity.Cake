using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorBadgeIconShape
{
    [Description("Circle")]
    Circle,

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
    : PropertyValueConverterBase<EditorBadgeIconShape>(dataTypeService)
{
    public override string DataTypeName => "Editor Badge Icon Shape";
}