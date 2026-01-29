namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

public interface ICoronerService
{
    IEnumerable<ICoronerInquestHearing> GetInquestHearing(string url, string authentication);
    IEnumerable<ICoronerInquestOpening> GetInquestOpening(string url, string authentication);
    IEnumerable<ICoronerInquestConclusion> GetInquestConclusion(string url, string authentication);
}
