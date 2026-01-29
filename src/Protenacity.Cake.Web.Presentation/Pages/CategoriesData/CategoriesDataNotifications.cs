using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Boot;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Pages.CategoriesData;

internal class CategoriesDataNotifications : NotificationBase<ContentSavingNotification>
{
    public override void Handle(ContentSavingNotification notification)
    {
        foreach (var entity in notification.SavedEntities.Where(n => n.ContentType.Alias == CategorysData.ModelTypeAlias))
        {
            SetAlias(entity, typeof(CategorysData), nameof(CategorysData.HeadingDescriptionStatus), 
                Enum<CategoryHeadingDescriptionStatuses>.GetDescriptionByValue(CategoryHeadingDescriptionStatuses.Hide), 
                Enum<CategoryHeadingDescriptionStatuses>.GetDescriptionByValue(CategoryHeadingDescriptionStatuses.Inherit));
        }
    }
}
