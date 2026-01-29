using System.Globalization;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

public class CoronerInquestOpening : CoronerInquestBase, ICoronerInquestOpening
{
    [JsonPropertyName("openingDate")]
    public string? OpeningDateField { get; init; }

    [JsonPropertyName("openingTime")]
    public string? OpeningTimeField { get; init; }

    [JsonIgnore]
    public DateTime OpeningDate => CalculateDate(OpeningDateField, OpeningTimeField);

    [JsonPropertyName("fullName")]
    public string? Fullname { get; init; }

    [JsonPropertyName("age")]
    public string? Age { get; init; }

    [JsonPropertyName("residence")]
    public string? Residence { get; init; }
}
