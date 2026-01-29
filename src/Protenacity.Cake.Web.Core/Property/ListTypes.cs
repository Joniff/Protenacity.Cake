using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum ListTypes
{
    [Description("Flexible Grid")]
    Grid,

    [Description("Carousel")]
    Carousel,

    [Description("Vertical Stepper")]
    StepperVertical,

    [Description("Horizontal Stepper")]
    StepperHorizontal
}
