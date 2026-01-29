using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IAsideTab
{
    AsideStatuses AsideStatusTyped { get; }
}

public partial class AsideTab
{
    public AsideStatuses AsideStatusTyped => Enum<AsideStatuses>.GetValueByDescription(this.AsideStatus);
}
