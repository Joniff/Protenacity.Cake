using Microsoft.AspNetCore.Hosting;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class WebHostEnvironmentExtensions
{
    //public static string? CompletePath(this IWebHostEnvironment webHostEnvironment, string? relativePath)
    //{
    //    return string.IsNullOrWhiteSpace(relativePath) ? null : Path.Combine(webHostEnvironment.WebRootPath, relativePath.Substring(1).Replace("/", "\\"));
    //}

    //public static string? CompletePath(this IWebHostEnvironment webHostEnvironment, IPublishedContent? media)
    //{
    //    var prop = media?.GetProperty(Umbraco.Cms.Core.Constants.Conventions.Media.File);
    //    return (prop?.HasValue() == true) ? webHostEnvironment.CompletePath(prop.GetValue() as string) : null;
    //}
}
