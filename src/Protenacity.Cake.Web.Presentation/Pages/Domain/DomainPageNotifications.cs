using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.Boot;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Pages.Domain;

internal class DomainPageNotifications : NotificationBase<ContentSavingNotification>
{
    public override void Handle(ContentSavingNotification notification)
    {
        foreach (var entity in notification.SavedEntities.Where(n => n.ContentType.Alias == DomainPage.ModelTypeAlias))
        {
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigTheme), "lancashire-county-council", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigImageQuality), 70, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigAsideLeftWidth), 3, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigAsideRightWidth), 3, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigUmbracoFormFieldName), "Name", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigUmbracoFormFieldMessage), "Message", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigUmbracoFormFieldNameDefault), "Anonymous", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigOpeningTimesCustomTimeFormat), "t", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchH1Boost), 100, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchH2Boost), 30, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchH3Boost), 10, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchH4Boost), 6, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchH5Boost), 3, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchH6Boost), 2, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchKeywordsBoost), 50, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchPromotedSearchTermsBoost), 200, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchImplementFuzzyResults), 1, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchFuzziness), 50, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigSearchSeoPriorities), "20,18,16,14,12,10,8,6,4,2,1", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigCoronerDateFormat), "d", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigCoronerTimeFormat), "t", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigRssFeedReaderCache), 15, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigCardDateFormat), "d", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigCardTimeFormat), "t", null);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigPageSizeDefault), 100, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigPageSizeMaximum), 1000, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigPageMaximum), 1000, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigCategoryCache), 60, 0);
            SetAlias(entity, typeof(DomainPage), nameof(DomainPage.ConfigDefaultImageMissingColor), "#D0D0D0", null);
        }
    }
}
