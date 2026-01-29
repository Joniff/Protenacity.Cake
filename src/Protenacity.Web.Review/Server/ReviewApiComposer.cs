using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Protenacity.Web.Review.Server;

public class ReviewApiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.ConfigureOptions<ReviewApiSwaggerGenOptions>();
    }
}

public class ReviewApiSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc(
            ReviewApiControllerBase.ApiName,
            new OpenApiInfo { Title = "Review Dashboard API", Version = "1.0" }
        );

        options.OperationFilter<ReviewApiOperationSecurityFilter>();
    }
}

public class ReviewApiOperationSecurityFilter : BackOfficeSecurityRequirementsOperationFilterBase
{
    protected override string ApiName => ReviewApiControllerBase.ApiName;
}