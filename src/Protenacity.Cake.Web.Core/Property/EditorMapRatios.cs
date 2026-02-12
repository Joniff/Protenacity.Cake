using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

// Divide enum / 36 = ratio as a decimal value
public enum EditorMapRatios
{
    [Description("21 : 9")]
    Ratio21x9 = 84,

    [Description("2 : 1")]
    Ratio2x1 = 72,

    [Description("16 : 9")]
    Ratio16x9 = 64,

    [Description("4 : 3")]
    Ratio4x3 = 48,

    [Description("1 : 1")]
    Ratio1x1 = 36,

    [Description("3 : 4")]
    Ratio3x4 = 27,

    [Description("1 : 2")]
    Ratio1x2 = 18,
}

public class EditorMapRatiosValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorMapRatios>(dataTypeService)
{

    public override string DataTypeName => "Editor Map Ratio";
}