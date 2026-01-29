using System.Globalization;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.Coroner;

public interface ICoronerInquestHearing : ICoronerInquest
{
    public DateTime ListingDate { get; }

    public string? Location { get; }

    public string? Fullname { get; init; }

    public string? Age { get; init; }

    public string? Residence { get; init; }

    public string? LengthOfHearing { get; init; }

    public string? HearingType { get; init; }
}
