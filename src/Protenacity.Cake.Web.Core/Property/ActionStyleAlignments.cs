using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionStyleAlignments
{
    //   Put Right at top so that it is default
    [Description("Right Absolute")]
    RightAbsolute = 17,

    [Description("Left Absolute")]
    LeftAbsolute = 18,

    [Description("Centre Absolute")]
    CentreAbsolute = 20,

    [Description("Right Relative")]
    RightRelative = 9,

    [Description("Left Relative")]
    LeftRelative = 10,

    [Description("Centre Relative")]
    CentreRelative = 12,

    // Mappings
    Absolute = 16,
    Relative = 8,
    Right = 1,
    Left = 2,
    Centre = 4
}
