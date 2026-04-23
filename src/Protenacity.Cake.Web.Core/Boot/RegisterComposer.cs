using Microsoft.Extensions.DependencyInjection;
using Protenacity.Cake.Web.Core.Cryptography;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Protenacity.Cake.Web.Core.Boot;

public class RegisterComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationAsyncHandler<UmbracoApplicationStartingNotification, Mcp.RegisterMcpClientHandler>();
        builder.Services.AddTransient<ICryptographyService, CryptographyService>();
    }
}
