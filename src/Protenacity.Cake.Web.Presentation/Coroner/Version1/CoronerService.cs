using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

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

    private IEnumerable<T> Get<T>(string url, string authentication, string stage, FusionCacheFactoryExecutionContext<IEnumerable<T>> ctx, CancellationToken ct) where T : ICoronerInquest
    {
        var httpClient = httpClientFactory.CreateClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(url),
            Content = new StringContent(JsonSerializer.Serialize(new CoronerRequest
            {
                Payload = new CoronerRequest.CoronerRequestPayload
                {
                    Stage = stage
                }
            }), Encoding.UTF8, MediaTypeNames.Application.Json)
        };
        //request.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ContentType, MediaTypeNames.Application.Json);
        //request.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.ContentEncoding, Encoding.UTF8.EncodingName);
        request.Headers.Add("API-Authentication", authentication);
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
                    var results = JsonSerializer.Deserialize<CoronerResponse<T>>(json);
                    if (results?.Payload == null)
                    {
                        logger.LogError("No payload from " + url + " for stage " + stage);
                    }
                    else if (results.Payload.Result != "success")
                    {
                        logger.LogError($"{results.Payload.Result} (Error {results.Payload.ErrorCode} {results.Payload.ErrorDescription}) from " + url + " for stage " + stage);
                    }
                    else if (results.Payload.Data?.Any() != true)
                    {
                        return Enumerable.Empty<T>();
                    }
                    else
                    {
                        return results.Payload.Data.Where(r => r.Data?.Any() == true).SelectMany(r => r.Data!);
                    }
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

    public IEnumerable<ICoronerInquestHearing> GetInquestHearing(string url, string authentication)
    {
        if (OperatingSystem.IsBrowser())
        {
            return Enumerable.Empty<CoronerInquestHearing>();
        }

        return fusionCache.GetOrSet<IEnumerable<CoronerInquestHearing>>(nameof(CoronerService) + nameof(Version1) + ":" + nameof(GetInquestHearing) + ":" + url + ":" + authentication, (ctx, ct) =>
        {
            return Get<CoronerInquestHearing>(url, authentication, "inquest_hearings", ctx, ct).OrderBy(r => r.ListingDate).ThenBy(r => r.Fullname);
        }, options => options.SetDuration(CacheLength));
    }

    public IEnumerable<ICoronerInquestOpening> GetInquestOpening(string url, string authentication)
    {
        if (OperatingSystem.IsBrowser())
        {
            return Enumerable.Empty<CoronerInquestOpening>();
        }
        return fusionCache.GetOrSet<IEnumerable<CoronerInquestOpening>>(nameof(CoronerService) + nameof(Version1) + ":" + nameof(GetInquestOpening) + ":" + url + ":" + authentication, (ctx, ct) =>
        {
            return Get<CoronerInquestOpening>(url, authentication, "inquest_openings", ctx, ct).OrderBy(r => r.OpeningDate).ThenBy(r => r.Fullname);
        }, options => options.SetDuration(CacheLength));
    }

    public IEnumerable<ICoronerInquestConclusion> GetInquestConclusion(string url, string authentication)
    {
        if (OperatingSystem.IsBrowser())
        {
            return Enumerable.Empty<CoronerInquestConclusion>();
        }

        return fusionCache.GetOrSet<IEnumerable<CoronerInquestConclusion>>(nameof(CoronerService) + nameof(Version1) + ":" + nameof(GetInquestConclusion) + ":" + url + ":" + authentication, (ctx, ct) =>
        {
            return Get<CoronerInquestConclusion>(url, authentication, "inquest_conclusions", ctx, ct).OrderBy(r => r.Fullname);
        }, options => options.SetDuration(CacheLength));
    }
}

