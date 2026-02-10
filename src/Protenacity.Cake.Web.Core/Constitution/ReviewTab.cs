using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IReviewTab
{
    ReviewStatuses ReviewStatusTyped { get; }

}

public partial class ReviewTab
{
    public ReviewStatuses ReviewStatusTyped => ReviewStatuses.ParseByDescription(this.ReviewStatus) ?? ReviewStatuses.Inherit;
}
