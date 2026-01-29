using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace Protenacity.Cake.Web.Presentation.Pages.Domain;

public class RssFeedComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<RssFeedMiddleware>();
        builder.Services.Configure<UmbracoPipelineOptions>(options =>
        {
            //options.AddFilter(new UmbracoPipelineFilter(nameof(RssFeedMiddleware),
            //    prePipeline: applicationBuilder => { applicationBuilder.UseMiddleware<RssFeedMiddleware>(); }

            //));
            options.AddFilter(new UmbracoPipelineFilter(nameof(RssFeedMiddleware),
                preRouting: applicationBuilder => { applicationBuilder.UseMiddleware<RssFeedMiddleware>(); }
            ));
        });
    }
}