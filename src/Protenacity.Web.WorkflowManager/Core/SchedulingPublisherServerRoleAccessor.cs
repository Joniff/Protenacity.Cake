using Umbraco.Cms.Core.Sync;

namespace Protenacity.Web.WorkflowManager.Core;

public class SchedulingPublisherServerRoleAccessor : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.SchedulingPublisher;
}