using System.Globalization;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version2;

public class CoronerInquestHearing : CoronerInquestBase, ICoronerInquestHearing
{
    [JsonPropertyName("listingDate")]
    public string? ListingDateField { get; init; }

    [JsonPropertyName("listingTime")]
    public string? ListingTimeField { get; init; }

    [JsonIgnore]
    public DateTime ListingDate => CalculateDate(ListingDateField, ListingTimeField);

    [JsonPropertyName("location")]
    public string? Location { get; init; }

    [JsonPropertyName("fullName")]
    public string? Fullname { get; init; }

    [JsonPropertyName("age")]
    public string? Age { get; init; }

    [JsonPropertyName("residence")]
    public string? Residence { get; init; }

    [JsonPropertyName("lengthOfHearing")]
    public string? LengthOfHearing { get; init; }

    [JsonPropertyName("hearingType")]
    public string? HearingType { get; init; }
}
