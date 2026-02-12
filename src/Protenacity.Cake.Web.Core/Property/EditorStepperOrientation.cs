using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorStepperOrientation
{
    [Description("Vertical")]
    Vertical,

    [Description("Horizontal")]
    Horizontal
}

public class EditorStepperOrientationValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorStepperOrientation>(dataTypeService)
{

    public override string DataTypeName => "Editor Stepper Orientation";
}