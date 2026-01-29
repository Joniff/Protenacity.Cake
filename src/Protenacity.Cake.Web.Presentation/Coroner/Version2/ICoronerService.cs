namespace Protenacity.Cake.Web.Presentation.Coroner.Version2;

public interface ICoronerService
{
    IEnumerable<ICoronerInquestHearing> GetInquestHearing(string url);
    IEnumerable<ICoronerInquestOpening> GetInquestOpening(string url);
    IEnumerable<ICoronerInquestConclusion> GetInquestConclusion(string url);
}
