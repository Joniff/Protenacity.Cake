using Protenacity.Cake.Web.Core.Proxy;
using ZiggyCreatures.Caching.Fusion;
using OpenIddict.Server.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.EnableProxy();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

builder.Services.AddFusionCache().WithOptions(opt =>
{
    opt.AutoRecoveryMaxItems = 1024;
}).WithDefaultEntryOptions(opt =>
{
    opt.Duration = TimeSpan.FromMinutes(1);
    opt.FactorySoftTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddOpenIddict().AddServer(options =>
{
    options.Configure(options => options.RequireProofKeyForCodeExchange = false);
});

builder.Services.Configure<OpenIddictServerAspNetCoreOptions>(options =>
{
    options.DisableTransportSecurityRequirement = true;
});

WebApplication app = builder.Build();
app.UseForwardedHeaders();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
