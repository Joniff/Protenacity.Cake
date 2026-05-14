using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Runtime;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;

namespace Protenacity.Web.WorkflowManager.Core;

internal class WorkflowManagerService(
    IServiceProvider serviceProvider
    ) : IWorkflowManagerService
{
    public bool CanEmail()
    {
        string reason;
        var runtimeState = serviceProvider.GetService<IRuntimeState>();
        var serverRoleAccessor = serviceProvider.GetService<IServerRoleAccessor>();
        var mainDom = serviceProvider.GetService<IMainDom>();

        if (runtimeState == null ||
            serverRoleAccessor == null ||
            mainDom == null)
        {
            return false;
        }

        var canRan = CanRun(runtimeState.Level, serverRoleAccessor.CurrentServerRole, mainDom.IsMainDom, out reason);

        System.Diagnostics.Debug.WriteLine(reason);
        return canRan;

    }

    private bool CanRun(
        RuntimeLevel runtimeLevel,
        ServerRole serverRole,
        bool isMainDom,
        out string reason)
    {
        reason = string.Empty;
        if (!isMainDom)
        {
            reason = "Does not run if not MainDom";
            return false;
        }
        if (runtimeLevel != RuntimeLevel.Run)
        {
            reason = "Umbraco is not running";
            return false;
        }
        if (serverRole != ServerRole.Unknown)
        {
            if (serverRole != ServerRole.Subscriber)
                return true;
            reason = "Does not run on replica servers";
            return false;
        }
        reason = "Does not run on servers with unknown role";
        return false;
    }

}
