using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class RedirectPage
{
    public SeoStatuses SeoStatusTyped => Enum<SeoStatuses>.GetValueByDescription(this.SeoStatus);
}
