using ZiggyCreatures.Caching.Fusion;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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

WebApplication app = builder.Build();

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
