using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Umbraco.Cms.Core.Routing;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class HttpRequestExtensions
{
    public static IPublishedRequest? UmbracoPublishedRequest(this HttpRequest request) =>
        request.HttpContext.Features.Get<Umbraco.Cms.Web.Common.Routing.UmbracoRouteValues>()?.PublishedRequest;

    public static Uri Uri(this HttpRequest request) =>
        new Uri(new Uri(new Uri(request.GetEncodedUrl()).GetLeftPart(UriPartial.Authority)),
            request.UmbracoPublishedRequest()?.AbsolutePathDecoded ?? request.Path.ToUriComponent());

    public static string Url(this HttpRequest request) => Uri(request).AbsoluteUri;

    private const string Media = "/media";

    public static bool IsUmbracoUrl(this HttpRequest request)
    {
        var excludes = new string[] {
                "/api/",
                Umbraco.Cms.Core.Constants.SystemDirectories.AppPlugins,
                "/install/",
                "/mini-profiler-resources/",
                "/umbraco/",
                Media
            };

        var path = request.Uri().PathAndQuery;
        return excludes.Any(u => path.StartsWith(u, StringComparison.InvariantCultureIgnoreCase));
    }

    public static bool IsMediaUrl(this HttpRequest request) => request.Uri().PathAndQuery.StartsWith(Media, StringComparison.InvariantCultureIgnoreCase);
}
