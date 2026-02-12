using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Boot;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Pages.CategoriesData;

internal class CategoryHeaderDataNotifications : NotificationBase<ContentSavingNotification>
{
    public override void Handle(ContentSavingNotification notification)
    {
        foreach (var entity in notification.SavedEntities.Where(n => n.ContentType.Alias == CategoryHeaderData.ModelTypeAlias))
        {
            SetAlias(entity, typeof(CategoryHeaderData), nameof(CategoryHeaderData.HeadingDescriptionStatus), 
                CategoryHeadingDescriptionStatuses.Inherit.Description ?? throw new ArgumentNullException("Missing Description attribute"), 
                CategoryHeadingDescriptionStatuses.Inherit.Description ?? throw new ArgumentNullException("Missing Description attribute"));
        }
    }
}
