using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

// Divide enum / 360 = ratio as a decimal value
public enum FurnitureLogoRatios
{
    [Description("5:1")]
    Ratio5x1 = 360 * 5,

    [Description("4:1")]
    Ratio4x1 = 360 * 4,

    [Description("3:1")]
    Ratio3x1 = 360 * 3,

    [Description("21:9")]
    Ratio21x9 = 360 * 21 / 9,

    [Description("2:1")]
    Ratio2x1 = 360 * 2,

    [Description("Golden Ratio (1.618)")]
    GoldenRatio = (int) (360.0 * 1.618),

    [Description("16:9")]
    Ratio16x9 = 360 * 16 / 9,

    [Description("4:3")]
    Ratio4x3 = 360 * 4 / 3,

    [Description("1:1")]
    Ratio1x1 = 360,

    [Description("3:4")]
    Ratio3x4 = 360 * 3 / 4,

    [Description("1:2")]
    Ratio1x2 = 360 / 2,
}

public class FurnitureLogoRatiosValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<FurnitureLogoRatios>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Furniture Logo Ratio";
}