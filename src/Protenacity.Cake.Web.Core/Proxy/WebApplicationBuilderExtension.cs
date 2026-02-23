using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Protenacity.Cake.Web.Core.Proxy;

static public class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder EnableProxy(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(nameof(Proxy)).Get<ProxySettings>();

        if (!string.IsNullOrWhiteSpace(settings?.IP4))
        {
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = 2;
                options.KnownIPNetworks.Add(new System.Net.IPNetwork(System.Net.IPAddress.Parse(settings.IP4), settings.Cidr ?? 32));
            });
        }

        return builder;
    }
}
