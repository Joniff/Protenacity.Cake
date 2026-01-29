using Protenacity.Cake.Web.Presentation.Search.BackgroundTask;
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

namespace Protenacity.Cake.Web.Presentation.Search.Core;

public class EditorIndexingNotificationHandler(IRuntimeState runtimeState,
    IUmbracoIndexingHandler umbracoIndexingHandler,
    IEditorSearchBackgroundTask editorSearchBackgroundTask) 
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
                editorSearchBackgroundTask.ScheduleExection(payload.Id);
            }
        }
    }
}
