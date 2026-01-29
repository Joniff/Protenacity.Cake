namespace Protenacity.Cake.Web.Presentation.Form;

public interface IFormReaderService
{
    FormReaderEntries ApprovedEntriesByDate(Guid form, string nameAlias, string defaultName, string messageAlias, int page = 0, int pagesize = 100, TimeSpan? cacheLength = null);
}
