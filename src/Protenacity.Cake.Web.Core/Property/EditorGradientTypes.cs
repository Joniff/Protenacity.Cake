using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorGradientTypes
{
    [Description("Linear gradient starting along top edge and finishing along bottom edge")]
    Top,

    [Description("Linear gradient starting along left edge and finishing along right edge")]
    Left,

    [Description("Linear gradient starting in top left corner and finishing in bottom right corner")]
    TopLeft,

    [Description("Radial gradient starting dead centre and radiating outwards")]
    Centre
}

public class EditorGradientTypesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorGradientTypes>(dataTypeService)
{

    public override string DataTypeName => "Editor Gradient Type";
}