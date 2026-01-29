using Microsoft.Extensions.Logging;
using Protenacity.Cake.Web.Core.Json;
using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class UriExtensions
{
    public static Uri AddParameter(this Uri url, params (string Name, string Value)[] @params)
    {
        if (!@params.Any())
        {
            return url;
        }

        UriBuilder uriBuilder = new(url);
        NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var param in @params)
        {
            query[param.Name] = param.Value.Trim();
        }

        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    public static T? Read<T>(this Uri url, IHttpClientFactory httpClientFactory, FusionCacheFactoryExecutionContext<T?> ctx, CancellationToken ct, TimeSpan cacheLength, ILogger logger)
    {
        var httpClient = httpClientFactory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = url
        };
        //request.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ContentType, MediaTypeNames.Application.Json);
        //request.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ContentEncoding, Encoding.UTF8.EncodingName);
        //request.Headers.Add("API-Authentication", authentication);
        try
        {
#pragma warning disable CA1416 // Invalidate platform compatibility
            var response = httpClient.Send(request, ct);
#pragma warning restore CA1416 // Validate platform compatibility

            if (!response.IsSuccessStatusCode)
            {
                //  API is down, return previous value
                logger.LogWarning("Unable to get latest Bus Route results from " + url);
                ctx.Options.Duration = cacheLength;
                try
                {
                    return ctx.NotModified();
                }
                catch
                {
                    return default(T);
                }
            }

            using (var reader = new StreamReader(response.Content.ReadAsStream()))
            {
                string json = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(json))
                {
                    logger.LogError("No response from " + url);
                }
                else
                {
                    var serializeOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };
                    serializeOptions.Converters.Add(new KeepJsonAsAStringValueJsonConverter());

                    try
                    {
                        return JsonSerializer.Deserialize<T>(json, serializeOptions);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Unable to parse " + url + ".\njson returned = \'" + json + "\'");
                    }
                }
                //  API is returning invalid results
                ctx.Options.Duration = cacheLength;
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error reading " + url.ToString(), ex);
        }
        try
        {
            return ctx.NotModified();
        }
        catch
        {
            return default(T);
        }
    }

}
