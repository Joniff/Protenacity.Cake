using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Presentation.BusRoute;
using Microsoft.Extensions.Logging;
using System.Globalization;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.BusTimeTable;

public class BusTimeTableService(IHttpClientFactory httpClientFactory,
    IFusionCache fusionCache,
    ILogger<BusRouteService> logger)
    : IBusTimeTableService
{
#if DEBUG
    private readonly TimeSpan CacheLength = TimeSpan.FromSeconds(1);
#else
    private readonly TimeSpan CacheLength = TimeSpan.FromDays(1);
#endif

    public IDictionary<string, string> ListServicesByStop(Uri url, string stop)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new Dictionary<string, string>();
        }

        var trimStop = stop.Trim();
        var key = nameof(BusTimeTable) + nameof(ListServicesByStop) + ":" + url.AbsoluteUri + ":" + trimStop;

        var results = fusionCache.GetOrSet<BusTimeTableServicesResponse?>(key, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("stopReferenceNo", trimStop)
                ).Read<BusTimeTableServicesResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            return data;
        }, options => options.SetDuration(CacheLength));

        return results?.Services.ToDictionary(k => k.Code, v => v.ServiceNo) ?? new Dictionary<string, string>();
    }

    public IEnumerable<DayOfWeek> ListDaysByServiceStop(Uri url, string service, string stop)
    {
        var days = new List<DayOfWeek>();

        if (OperatingSystem.IsBrowser())
        {
            return days;
        }

        var trimService = service.Trim();
        var trimStop = stop.Trim();
        var key = nameof(BusTimeTable) + nameof(ListDaysByServiceStop) + ":" + url.AbsoluteUri + ":" + trimService + ":" + trimStop;

        var response = fusionCache.GetOrSet<BusTimeTableDaysResponse?>(key, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("serviceCode", trimService),
                    ("stopReferenceNo", trimStop)
                ).Read<BusTimeTableDaysResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            return data;
        }, options => options.SetDuration(CacheLength));

        if (response?.OperationalDays.Any() == true)
        {
            foreach (var day in response.OperationalDays)
            {
                if (int.TryParse(day.Code, out var code))
                {
                    var dayOfWeek = DayOfWeek.Monday;
                    switch (code)
                    {
                        case 3:
                            dayOfWeek = DayOfWeek.Monday;
                            break;

                        case 5:
                            dayOfWeek = DayOfWeek.Saturday;
                            break;

                        case 6:
                            dayOfWeek = DayOfWeek.Sunday;
                            break;

                        default:
                            // Unknown Value
                            break;
                    }
                    days.Add(dayOfWeek);
                }
            }
        }

        return days;
    }

    public IDictionary<DateTime, string> ListTimeTableByServiceStopDay(Uri url, string service, string stop, DayOfWeek day)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new Dictionary<DateTime, string>();
        }

        var trimService = service.Trim();
        var trimStop = stop.Trim();
        var key = nameof(BusTimeTable) + nameof(ListTimeTableByServiceStopDay) + ":" + url.AbsoluteUri + ":" + trimService + ":" + trimStop + ":" + day;

        var response = fusionCache.GetOrSet<BusTimeTableTimesResponse?>(key, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("serviceCode", trimService),
                    ("operationalDayCode", day == DayOfWeek.Sunday ? "6" : day == DayOfWeek.Saturday ? "5" : "3"),
                    ("stopReferenceNo", trimStop)
                ).Read<BusTimeTableTimesResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            return data;
        }, options => options.SetDuration(CacheLength));

        var provider = new CultureInfo("en-GB");
        return response?.Times.GroupBy(t => t.Time).ToDictionary(t => DateTime.ParseExact(t.Key, "HH:mm", provider), v => v.First().Notes) ?? new Dictionary<DateTime, string>();
    }
}
