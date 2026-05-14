using Umbraco.Cms.Core.Sync;

namespace Protenacity.Web.WorkflowManager.Core;

public class SingleServerRoleAccessor : IServerRoleAccessor
{
    public ServerRole CurrentServerRole => ServerRole.Single;
}
