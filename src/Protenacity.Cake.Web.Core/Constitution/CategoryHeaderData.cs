using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class CategoryHeaderData
{
    public CategoryHeadingDescriptionStatuses HeadingDescriptionStatusTyped => CategoryHeadingDescriptionStatuses.ParseByDescription(this.HeadingDescriptionStatus.ToString()) ?? CategoryHeadingDescriptionStatuses.Hide;
}
