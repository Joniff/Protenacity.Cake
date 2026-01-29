using Protenacity.Cake.Web.Core.Json;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.BusRoute;

public class BusRouteResponse
{
    public class Field
    {
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }

        [JsonPropertyName("alias")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Alias { get; set; }

        [JsonPropertyName("length")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Length { get; set; }
    }

    [JsonPropertyName("fields")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Field>? Fields { get; set; }

    public class Feature
    {
        public class AttributeRecord
        {
            [JsonPropertyName("FIRST_BUSN")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Id { get; set; }

            [JsonPropertyName("SERVICE")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? ServiceIda { get; set; }

            [JsonPropertyName("SERVICES")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? ServiceIdb { get; set; }

            [JsonIgnore]
            public string? ServiceId
            {
                get
                {
                    return ServiceIda ?? ServiceIdb;
                }
                set
                {
                    ServiceIda = ServiceIdb = value;
                }
            }

            [JsonPropertyName("FIRST_ROUT")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Route { get; set; }

            [JsonPropertyName("DISTRICT")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? District { get; set; }

            [JsonPropertyName("LOCALITY")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Locality { get; set; }

            [JsonPropertyName("POSTCODE")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Postcode { get; set; }

            [JsonPropertyName("ROAD_NAME")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Address1a { get; set; }

            [JsonPropertyName("X_MAINRD")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Address1b { get; set; }

            [JsonIgnore]
            public string? Address1
            {
                get
                {
                    return Address1a ?? Address1b;
                }
                set
                {
                    Address1a = Address1b = value;
                }
            }

            [JsonPropertyName("ADDRESS2")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Address2 { get; set; }

            [JsonPropertyName("X_STOPREF")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? StopId { get; set; }

            [JsonPropertyName("X_NAME")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Stop { get; set; }

            [JsonPropertyName("STATUS")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Status { get; set; }
        }

        [JsonPropertyName("attributes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AttributeRecord? Attribute { get; set; }

        public class GeometryRecord
        {
            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("extent")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Extent { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("spatialReference")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? SpatialReference { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("hasM")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? HasM { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("centroid")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Centroid { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("curveRings")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? CurveRings { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("hasZ")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? HasZ { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("isSelfIntersecting")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? IsSelfIntersecting { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("rings")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Rings { get; set; }

            [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
            [JsonPropertyName("type")]
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Type { get; set; }
        }

        [JsonPropertyName("geometry")]
        public GeometryRecord? Geometry { get; set; }
    }

    [JsonPropertyName("features")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Feature>? Features { get; set; }

    [JsonPropertyName("geometryType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GeometryType { get; set; }

    [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
    [JsonPropertyName("hasM")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HasM { get; set; }

    [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
    [JsonPropertyName("hasZ")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? HasZ { get; set; }

    [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
    [JsonPropertyName("spatialReference")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SpatialReference { get; set; }

    [JsonPropertyName("exceededTransferLimit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ExceededTransferLimit { get; set; }

    [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
    [JsonPropertyName("geometryProperties")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GeometryProperties { get; set; }

    [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
    [JsonPropertyName("uniqueIdField")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UniqueIdField { get; set; }

    [JsonPropertyName("objectIdFieldName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ObjectIdFieldName { get; set; }

    public class ErrorRecord
    {
        [JsonPropertyName("code")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Code { get; set; }

        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonPropertyName("details")]
        [JsonConverter(typeof(KeepJsonAsAStringValueJsonConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Details { get; set; }
    }

    [JsonPropertyName("error")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorRecord? Error { get; set; }
}

