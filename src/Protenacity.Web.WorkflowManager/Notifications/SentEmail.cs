using Umbraco.Cms.Core.Events;
using Umbraco.Workflow.Core.Email.Notifications;

namespace Protenacity.Web.WorkflowManager.Notifications;

internal class SentEmail : INotificationHandler<WorkflowEmailNotificationsSentNotification>
{
    public void Handle(WorkflowEmailNotificationsSentNotification notification)
    {
        //throw new NotImplementedException();
    }
}
