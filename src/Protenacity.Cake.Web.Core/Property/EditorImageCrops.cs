using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorImageCrops
{
    [Description("Full Screen Image")]
    Full = 0,

    [Description("Hero Banner")]
    Banner = 1,

    [Description("Poster")]
    Poster = 2,

    [Description("Card")]
    Card = 3,

    [Description("Logo")]
    Logo = 4
}
