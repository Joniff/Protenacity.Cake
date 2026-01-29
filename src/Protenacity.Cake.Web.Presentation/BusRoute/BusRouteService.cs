using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Json;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.BusRoute;

public class BusRouteService(IHttpClientFactory httpClientFactory,
    IFusionCache fusionCache,
    ILogger<BusRouteService> logger)
    : IBusRouteService
{
#if DEBUG
    private readonly TimeSpan CacheLength = TimeSpan.FromSeconds(1);
#else
    private readonly TimeSpan CacheLength = TimeSpan.FromDays(1);
#endif

    // https://services-eu1.arcgis.com/9MmxkLJT84uEwsJx/arcgis/rest/services/Bus_Routes/FeatureServer/0/query?where=FIRST_BUSN+LIKE+%27%2525%25%27&objectIds=&geometry=&geometryType=esriGeometryEnvelope&inSR=&spatialRel=esriSpatialRelIntersects&resultType=none&distance=0.0&units=esriSRUnit_Meter&relationParam=&returnGeodetic=false&outFields=FIRST_BUSN%2CSERVICE%2CFIRST_ROUT&returnGeometry=false&returnEnvelope=false&featureEncoding=esriDefault&multipatchOption=xyFootprint&maxAllowableOffset=&geometryPrecision=&outSR=&defaultSR=&datumTransformation=&applyVCSProjection=false&returnIdsOnly=false&returnUniqueIdsOnly=false&returnCountOnly=false&returnExtentOnly=false&returnQueryGeometry=false&returnDistinctValues=false&cacheHint=false&collation=&orderByFields=&groupByFieldsForStatistics=&outStatistics=&having=&resultOffset=&resultRecordCount=&returnZ=false&returnM=false&returnTrueCurves=false&returnExceededLimitFeatures=true&quantizationParameters=&sqlFormat=none&f=pjson&token=
    public BusRouteResponse? SearchBusRoutes(Uri url, string criteria)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var routeName = criteria.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(SearchBusRoutes) + ":" + url.AbsoluteUri + ":" + routeName, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "FIRST_BUSN LIKE '%" + routeName + "%'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "false"),
                    ("outFields", "FIRST_BUSN,SERVICE,FIRST_ROUT"),
                    ("returnGeometry", "false"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", "FIRST_BUSN"),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength,logger);

            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"SearchBusRoutes({url},{criteria}) = {data.Error.Message}");
            return null;
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? GetDistricts(Uri url)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetDistricts) + ":" + url.AbsoluteUri, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "DISTRICT IN ('BURNLEY','CHORLEY','FYLDE','HYNDBURN','LANCASTER','PENDLE','PRESTON','RIBBLE VALLEY','ROSSENDALE','SOUTH RIBBLE','WEST LANCASHIRE','WYRE')"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "DISTRICT,LOCALITY"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetDistricts({url}) = {data.Error.Message}");
            return null;

        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? SearchLocations(Uri url, string criteria)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var routeName = criteria.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(SearchLocations) + ":" + url.AbsoluteUri + ":" + routeName, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "FIRST_BUSN LIKE '%" + routeName + "%'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "FIRST_BUSN,SERVICE,FIRST_ROUT"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"SearchLocations({url}, {criteria}) = {data.Error.Message}");
            return null;

        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? GetBusRoute(Uri url, string criteria)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var serviceId = criteria.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetBusRoute) + ":" + url.AbsoluteUri + ":" + serviceId, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "SERVICE='" + serviceId + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "FIRST_BUSN,SERVICE,FIRST_ROUT"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetBusRoute({url}, {criteria}) = {data.Error.Message}");
            return null;
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? GetBusService(Uri url, string criteria)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var serviceId = criteria.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetBusService) + ":" + url.AbsoluteUri + ":" + serviceId, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "SERVICE='" + serviceId + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "FIRST_BUSN,SERVICE,FIRST_ROUT"),
                    ("returnGeometry", "false"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetBusService({url}, {criteria}) = {data.Error.Message}");
            return null;
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? GetPostcode(Uri url, string criteria)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var postcode = criteria.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetPostcode) + ":" + url.AbsoluteUri + ":" + postcode, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "POSTCODE = '" + postcode + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "POSTCODE"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetPostcode({url}, {criteria}) = {data.Error.Message}");
            return null;

        }, options => options.SetDuration(CacheLength));
    }

    private string GetAddressWhereClause(string address)
    {
        var seperator = address.Split(new char[] { ',', '|', '\t', ';' });
        if (seperator.Length > 1)
        {
            return "ROAD_NAME = '" + seperator[0].Trim() + "' AND ADDRESS2 = '" + seperator[1].Trim() + "'";
        }
        return "ROAD_NAME LIKE '%" + address + "%' OR ADDRESS2 LIKE '%" + address + "%'";
    }

    public BusRouteResponse? GetAddress(Uri url, string criteria)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var address = criteria.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetAddress) + ":" + url.AbsoluteUri + ":" + address, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", GetAddressWhereClause(address)),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "ROAD_NAME,ADDRESS2"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetAddress({url}, {criteria}) = {data.Error.Message}");
            return null;
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? GetBusStops(Uri url, string route)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var routeTrim = route.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetBusStops) + ":" + url.AbsoluteUri + ":" + routeTrim, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "SERVICES = '" + routeTrim + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "false"),
                    ("outFields", "X_STOPREF,X_NAME,X_MAINRD,LOCALITY,SERVICES,STATUS"),
                    ("returnGeometry", "false"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetBusStops({url}, {routeTrim}) = {data.Error.Message}");
            return null;
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteResponse? GetBusStop(Uri url, string busStop)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteResponse();
        }

        var busStopTrim = busStop.Trim();

        return fusionCache.GetOrSet<BusRouteResponse?>(nameof(GetBusStop) + ":" + url.AbsoluteUri + ":" + busStopTrim, (ctx, ct) =>
        {
            var data = url
                .AddParameter(
                    ("where", "X_STOPREF = '" + busStopTrim + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "true"),
                    ("outFields", "X_STOPREF,X_NAME"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "false"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
            if (data?.Error == null)
            {
                return data;
            }
            logger.LogError($"GetBusStops({url}, {busStopTrim}) = {data.Error.Message}");
            return null;
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteExtentResponse? GetServiceMapExtent(Uri url, string serviceId)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteExtentResponse();
        }

        var serviceIdTrim = serviceId.Trim();

        return fusionCache.GetOrSet<BusRouteExtentResponse?>(nameof(GetServiceMapExtent) + ":" + url.AbsoluteUri + ":" + serviceIdTrim, (ctx, ct) =>
        {
            return url
                .AddParameter(
                    ("where", "SERVICE='" + serviceIdTrim + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "false"),
                    ("outFields", "FIRST_BUSN,SERVICE,FIRST_ROUT"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "true"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteExtentResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
        }, options => options.SetDuration(CacheLength));
    }

    public BusRouteExtentResponse? GetBusStopMapExtent(Uri url, string busStopId)
    {
        if (OperatingSystem.IsBrowser())
        {
            return new BusRouteExtentResponse();
        }

        var busStopIdTrim = busStopId.Trim();

        return fusionCache.GetOrSet<BusRouteExtentResponse?>(nameof(GetServiceMapExtent) + ":" + url.AbsoluteUri + ":" + busStopIdTrim, (ctx, ct) =>
        {
            return url
                .AddParameter(
                    ("where", "X_STOPREF='" + busStopIdTrim + "'"),
                    ("objectIds", ""),
                    ("geometry", ""),
                    ("geometryType", "esriGeometryEnvelope"),
                    ("inSR", ""),
                    ("spatialRel", "esriSpatialRelIntersects"),
                    ("resultType", "none"),
                    ("distance", "0.0"),
                    ("units", "esriSRUnit_Meter"),
                    ("relationParam", ""),
                    ("returnGeodetic", "false"),
                    ("outFields", "X_STOPREF,X_NAME"),
                    ("returnGeometry", "true"),
                    ("returnEnvelope", "false"),
                    ("featureEncoding", "esriDefault"),
                    ("multipatchOption", "xyFootprint"),
                    ("maxAllowableOffset", ""),
                    ("geometryPrecision", ""),
                    ("outSR", ""),
                    ("defaultSR", ""),
                    ("datumTransformation", ""),
                    ("applyVCSProjection", "false"),
                    ("returnIdsOnly", "false"),
                    ("returnUniqueIdsOnly", "false"),
                    ("returnCountOnly", "false"),
                    ("returnExtentOnly", "true"),
                    ("returnQueryGeometry", "false"),
                    ("returnDistinctValues", "false"),
                    ("cacheHint", "false"),
                    ("collation", ""),
                    ("orderByFields", ""),
                    ("groupByFieldsForStatistics", ""),
                    ("outStatistics", ""),
                    ("having", ""),
                    ("resultOffset", ""),
                    ("resultRecordCount", ""),
                    ("returnZ", "false"),
                    ("returnM", "false"),
                    ("returnTrueCurves", "false"),
                    ("returnExceededLimitFeatures", "true"),
                    ("quantizationParameters", ""),
                    ("sqlFormat", "none"),
                    ("f", "pjson"),
                    ("token", "")
                ).Read<BusRouteExtentResponse>(httpClientFactory, ctx, ct, CacheLength, logger);
        }, options => options.SetDuration(CacheLength));
    }

}
