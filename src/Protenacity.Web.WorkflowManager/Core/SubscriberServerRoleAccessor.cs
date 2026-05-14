using Umbraco.Cms.Core.Sync;

namespace Protenacity.Web.WorkflowManager.Core;

public class SubscriberServerRoleAccessor : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Subscriber;
}
