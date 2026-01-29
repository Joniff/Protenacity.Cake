using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

public class CoronerRequest
{
    public class CoronerRequestPayload
    {
        [JsonPropertyName("client_unique_identifier")]
        public Guid Id { get; init; } = Guid.NewGuid();

        [JsonPropertyName("function")]
        public required string Stage { get; init; }

    }

    [JsonPropertyName("payload")]
    public required CoronerRequestPayload Payload { get; init; }
}
