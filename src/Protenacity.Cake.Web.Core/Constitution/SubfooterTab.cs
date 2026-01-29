using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface ISubfooterTab
{
    SubfooterStatuses SubfooterStatusTyped { get; }
}

public partial class SubfooterTab
{
    public SubfooterStatuses SubfooterStatusTyped => Enum<SubfooterStatuses>.GetValueByDescription(this.SubfooterStatus);
}
