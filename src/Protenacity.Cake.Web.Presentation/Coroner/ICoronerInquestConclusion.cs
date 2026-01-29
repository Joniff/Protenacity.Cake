namespace Protenacity.Cake.Web.Presentation.Coroner;

public interface ICoronerInquestConclusion : ICoronerInquest
{
    public DateTime ListingDate { get; }

    public DateTime ConcludedDate { get; }

    public string? Location { get; }

    public string? Fullname { get; }

    public string? LengthOfHearing { get; }

    public string? HearingConclusions { get; }
}
