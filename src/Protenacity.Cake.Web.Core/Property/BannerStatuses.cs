using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum BannerStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Show Hero Banners")]
    ShowBanners,

    [Description("Show Single Image")]
    ShowImage,

    [Description("Hide")]
    Hide
}
