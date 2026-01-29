using Microsoft.Extensions.DependencyInjection;
using Protenacity.Web.GraphEmail.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Mail;

namespace Protenacity.Web.GraphEmail.Boot;

public class RegisterComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Remove(new ServiceDescriptor(typeof(IEmailSender), typeof(Umbraco.Cms.Infrastructure.Mail.EmailSender), ServiceLifetime.Singleton));
        builder.Services.AddSingleton<IEmailSender, GraphEmailSender>();
    }
}
