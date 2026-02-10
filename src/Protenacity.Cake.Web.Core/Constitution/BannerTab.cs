using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IBannerTab
{
    BannerStatuses BannerStatusTyped { get; }
}

public partial class BannerTab
{
    public BannerStatuses BannerStatusTyped => BannerStatuses.ParseByDescription(this.BannerStatus) ?? BannerStatuses.Hide;
}
