using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IVirtualAgentTab
{
    VirtualAgentStatuses VirtualAgentStatusTyped { get; }
}

public partial class VirtualAgentTab
{
    public VirtualAgentStatuses VirtualAgentStatusTyped => Enum<VirtualAgentStatuses>.GetValueByDescription(this.VirtualAgentStatus);
}
