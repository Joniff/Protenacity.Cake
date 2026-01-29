using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class CategoryHeaderData
{
    public CategoryHeadingDescriptionStatuses HeadingDescriptionStatusTyped => Enum<CategoryHeadingDescriptionStatuses>.GetValueByDescription(this.HeadingDescriptionStatus);
}
