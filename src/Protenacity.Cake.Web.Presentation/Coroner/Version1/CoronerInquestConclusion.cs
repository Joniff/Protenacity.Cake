using System.Globalization;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

public class CoronerInquestConclusion : CoronerInquestBase, ICoronerInquestConclusion
{
    [JsonPropertyName("listingDate")]
    public string? ListingDateField { get; init; }

    [JsonPropertyName("listingTime")]
    public string? ListingTimeField { get; init; }

    [JsonIgnore]
    public DateTime ListingDate => CalculateDate(ListingDateField, ListingTimeField);

    [JsonPropertyName("concludedDate")]
    public string? ConcludedDateField { get; init; }

    [JsonPropertyName("concludedTime")]
    public string? ConcludedTimeField { get; init; }

    [JsonIgnore]
    public DateTime ConcludedDate => CalculateDate(ConcludedDateField, ConcludedTimeField);

    [JsonPropertyName("location")]
    public string? Location { get; init; }

    [JsonPropertyName("fullName")]
    public string? Fullname { get; init; }

    [JsonPropertyName("lengthOfHearing")]
    public string? LengthOfHearing { get; init; }

    [JsonPropertyName("hearingConclusions")]
    public string? HearingConclusions { get; init; }
}
