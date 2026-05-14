using Protenacity.Web.WorkflowManager.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Workflow.Core.Email.Notifications;

namespace Protenacity.Web.WorkflowManager.Notifications;

internal class SendingEmail(IWorkflowManagerService workflowManagerService) : INotificationHandler<WorkflowEmailNotificationsSendingNotification>
{
    public void Handle(WorkflowEmailNotificationsSendingNotification notification)
    {
        workflowManagerService.CanEmail();
        //throw new NotImplementedException();
    }
}