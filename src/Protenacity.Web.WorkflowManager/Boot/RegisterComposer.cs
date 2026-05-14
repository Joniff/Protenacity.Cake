using Microsoft.Extensions.DependencyInjection;
using Protenacity.Web.WorkflowManager.Core;
using Protenacity.Web.WorkflowManager.Notifications;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.DependencyInjection;
using Umbraco.Workflow.Core.Email.Notifications;

namespace Protenacity.Web.WorkflowManager.Boot;

public class RegisterComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<IWorkflowManagerService, WorkflowManagerService>();
        builder.AddNotificationHandler<WorkflowEmailNotificationsSendingNotification, SendingEmail>();
        builder.AddNotificationHandler<WorkflowEmailNotificationsSentNotification, SentEmail>();
        SetServerRole(builder);
    }

    private void SetServerRole(IUmbracoBuilder builder)
    {
        var serverRole = (Environment.GetEnvironmentVariable("UMBRACO_SERVER_ROLE") 
            ?? builder.Config.GetSection("Umbraco:CMS:ServerRole").Value)?.ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(serverRole) || serverRole.IndexOf("single") != -1)
        {
            builder.SetServerRegistrar<SingleServerRoleAccessor>();
        }
        else if (serverRole.IndexOf("subscriber") != -1 || serverRole.IndexOf("front") != -1)
        {
            builder.SetServerRegistrar<SubscriberServerRoleAccessor>();
        }
        else
        {
            builder.SetServerRegistrar<SchedulingPublisherServerRoleAccessor>();
        }
    }
}
