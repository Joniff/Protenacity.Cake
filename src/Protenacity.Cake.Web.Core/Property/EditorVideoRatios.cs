using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorVideoRatios
{
    [Description("16:9 (1280x720 or 1920x1080)")]
    SixteenByNine,

    [Description("21:9 (2650x1080 or 3440x1440)")]
    TwentyOneByNine,

    [Description("4:3 (640x480)")]
    FourByThree,

    [Description("1:1 (1024x1024)")]
    OneByOne
}
