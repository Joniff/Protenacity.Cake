using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

public class CoronerResponse<T> where T : ICoronerInquest
{
    public class CoronerResponsePayload
    {
        [JsonPropertyName("client_unique_identifier")]
        public required string Id { get; init; }

        [JsonPropertyName("result")]
        public string? Result { get; init; }

        [JsonPropertyName("error_code")]
        public string? ErrorCode { get; init; }

        [JsonPropertyName("error_desc")]
        public string? ErrorDescription { get; init; }

        public class CoronerResponsePayloadData
        {
            [JsonPropertyName("result")]
            public string? Result { get; init; }

            [JsonPropertyName("error_code")]
            public string? ErrorCode { get; init; }

            [JsonPropertyName("error_desc")]
            public string? ErrorDescription { get; init; }

            [JsonPropertyName("data")]
            public required IEnumerable<T>? Data { get; init; }
        }

        [JsonPropertyName("data")]
        public required IEnumerable<CoronerResponsePayloadData>? Data { get; init; }

        [JsonPropertyName("condition")]
        public string? Condition { get; init; }
    }

    [JsonPropertyName("payload")]
    public required CoronerResponsePayload Payload { get; init; }

}
