using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.View;

public class VideoService : IVideoService
{
    public string? YouTube(string url)
    {
        var uri = new Uri(new Uri("https://youtube.com"), url);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

        if (query["v"] != null)
        {
            return query["v"];
        }

        var slugs = uri.LocalPath.Split('/');
        return slugs.LastOrDefault();
    }

    public string? Vimeo(string url)
    {
        var uri = new Uri(new Uri("https://vimeo.com"), url);
        var slugs = uri.LocalPath.Split('/');
        return slugs.LastOrDefault();
    }


    public string? Media(MediaWithCrops mediaWithCrops)
    {
        return mediaWithCrops.Url();
    }

    public string? Mp4(string url)
    {
        return url;
    }
}
