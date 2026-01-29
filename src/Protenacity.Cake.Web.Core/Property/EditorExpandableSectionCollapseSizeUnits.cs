using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorExpandableSectionCollapseSizeUnits
{
    [Description("Screen Percentage")]
    ScreenPercentage,

    [Description("Section Percentage")]
    SectionPercentage,

    [Description("Fixed Height")]
    FixedHeight
}
