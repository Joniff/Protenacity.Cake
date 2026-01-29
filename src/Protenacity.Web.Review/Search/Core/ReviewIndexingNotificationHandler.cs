using Protenacity.Web.Review.Search.BackgroundTask;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Services.Changes;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure;
using Umbraco.Cms.Infrastructure.Search;
using Umbraco.Extensions;

namespace Protenacity.Web.Review.Search.Core;

public class ReviewIndexingNotificationHandler(IRuntimeState runtimeState,
    IUmbracoIndexingHandler umbracoIndexingHandler,
    IReviewSearchBackgroundTask reviewSearchBackgroundTask) 
    : INotificationHandler<ContentCacheRefresherNotification>
{
    private bool NotificationHandlingEnabled() => runtimeState.Level == RuntimeLevel.Run && umbracoIndexingHandler.Enabled == true && Suspendable.ExamineEvents.CanIndex == true;

    public void Handle(ContentCacheRefresherNotification notification)
    {
        if (!NotificationHandlingEnabled())
        {
            return;
        }

        if (notification.MessageType != MessageType.RefreshByPayload ||
            notification.MessageObject is not ContentCacheRefresher.JsonPayload[] payloads)
        {
            throw new NotSupportedException();
        }

        foreach (var payload in payloads)
        {
            if (payload.ChangeTypes.HasType(TreeChangeTypes.RefreshNode) ||
                payload.ChangeTypes.HasType(TreeChangeTypes.RefreshBranch) ||
                payload.ChangeTypes.HasType(TreeChangeTypes.Remove))
            {
                reviewSearchBackgroundTask.ScheduleExection(payload.Id);
            }
        }
    }
}
