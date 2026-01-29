namespace Protenacity.Cake.Web.Presentation.Coroner;

public interface ICoronerInquestOpening : ICoronerInquest
{
    public DateTime OpeningDate { get; }

    public string? Fullname { get; }

    public string? Age { get; }

    public string? Residence { get; }
}
