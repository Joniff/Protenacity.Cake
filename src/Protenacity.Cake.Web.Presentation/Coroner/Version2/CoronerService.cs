using Microsoft.Extensions.Logging;
using System.Text.Json;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version2;

internal class CoronerService(IHttpClientFactory httpClientFactory,
    IFusionCache fusionCache,
    ILogger<CoronerService> logger)
    : ICoronerService
{
#if DEBUG
    private readonly TimeSpan CacheLength = TimeSpan.FromSeconds(1);
#else
    private readonly TimeSpan CacheLength = TimeSpan.FromMinutes(10);
#endif

    private IEnumerable<T> Get<T>(string url, string stage, FusionCacheFactoryExecutionContext<IEnumerable<T>> ctx, CancellationToken ct) where T : ICoronerInquest
    {
        var httpClient = httpClientFactory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url.Replace('\\', '/').EnsureEndsWith('/') + stage),
        };
        //request.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ContentType, MediaTypeNames.Application.Json);
        //request.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ContentEncoding, Encoding.UTF8.EncodingName);
#pragma warning disable CA1416 // Validate platform compatibility
        var response = httpClient.Send(request, ct);
#pragma warning restore CA1416 // Validate platform compatibility

        if (!response.IsSuccessStatusCode)
        {
            //  API is down, return previous value
            logger.LogWarning("Unable to get latest Coroner Inquest results from " + url + " for stage " + stage);
            ctx.Options.Duration = CacheLength;
            try
            {
                return ctx.NotModified();
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }

        using (var reader = new StreamReader(response.Content.ReadAsStream()))
        {
            string json = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(json))
            {
                logger.LogError("No response from " + url + " for stage " + stage);
            }
            else
            {
                try
                {
                    var results = JsonSerializer.Deserialize<IEnumerable<T>>(json);
                    return (results?.Any() == true) ? results : Enumerable.Empty<T>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unable to parse latest Coroner Inquest results from " + url + " for stage " + stage + ".\njson returned = \'" + json + "\'");
                }
            }
            //  API is returning invalid results
            ctx.Options.Duration = CacheLength;
            try
            {
                return ctx.NotModified();
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }
    }

    public IEnumerable<ICoronerInquestHearing> GetInquestHearing(string url)
    {
        if (OperatingSystem.IsBrowser())
        {
            return Enumerable.Empty<ICoronerInquestHearing>();
        }

        return fusionCache.GetOrSet<IEnumerable<CoronerInquestHearing>>(nameof(CoronerService) + nameof(Version2) + ":" + nameof(GetInquestHearing) + ":" + url + ":", (ctx, ct) =>
        {
            return Get<CoronerInquestHearing>(url, "inquest_hearings", ctx, ct).OrderBy(r => r.ListingDate).ThenBy(r => r.Fullname);
        }, options => options.SetDuration(CacheLength));
    }

    public IEnumerable<ICoronerInquestOpening> GetInquestOpening(string url)
    {
        if (OperatingSystem.IsBrowser())
        {
            return Enumerable.Empty<CoronerInquestOpening>();
        }

        return fusionCache.GetOrSet<IEnumerable<CoronerInquestOpening>>(nameof(CoronerService) + nameof(Version2) + ":" + nameof(GetInquestOpening) + ":" + url, (ctx, ct) =>
        {
            return Get<CoronerInquestOpening>(url, "inquest_openings", ctx, ct).OrderBy(r => r.OpeningDate).ThenBy(r => r.Fullname);
        }, options => options.SetDuration(CacheLength));
    }

    public IEnumerable<ICoronerInquestConclusion> GetInquestConclusion(string url)
    {
        if (OperatingSystem.IsBrowser())
        {
            return Enumerable.Empty<CoronerInquestConclusion>();
        }

        return fusionCache.GetOrSet<IEnumerable<CoronerInquestConclusion>>(nameof(CoronerService) + nameof(Version2) + ":" + nameof(GetInquestConclusion) + ":" + url, (ctx, ct) =>
        {
            return Get<CoronerInquestConclusion>(url, "inquest_conclusions", ctx, ct).OrderBy(r => r.Fullname);
        }, options => options.SetDuration(CacheLength));
    }
}

