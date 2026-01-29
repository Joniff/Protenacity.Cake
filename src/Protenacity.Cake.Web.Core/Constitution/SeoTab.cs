using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface ISeoTab
{
    SeoStatuses SeoStatusTyped { get; }

}

public partial class SeoTab
{
    public SeoStatuses SeoStatusTyped => Enum<SeoStatuses>.GetValueByDescription(this.SeoStatus);
}