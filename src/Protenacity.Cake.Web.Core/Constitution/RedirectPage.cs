using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class RedirectPage
{
    public SeoStatuses SeoStatusTyped => SeoStatuses.ParseByDescription(this.SeoStatus) ?? SeoStatuses.Inherit;
}
