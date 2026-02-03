using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Form;

public class FormReaderService(
    IFusionCache fusionCache) 
    : IFormReaderService
{
    static readonly TimeSpan DefaultCacheLength = new TimeSpan(TimeSpan.TicksPerHour);

    public FormReaderEntries ApprovedEntriesByDate(Guid form, string nameAlias, string defaultName, string messageAlias, int page = 0, int pagesize = 100, TimeSpan? cacheLength = null)
    {
        var cacheKey = nameof(ApprovedEntriesByDate) + form.ToString("N") + ":" + page.ToString() + ":" + pagesize.ToString();

        return fusionCache.GetOrSet<FormReaderEntries>(cacheKey, _ =>
        {
            //var approved = recordReaderService.GetApprovedRecordsFromForm(form, page + 1, pagesize);

            // Umbraco 15 = approved.TotalItems
            // Umbraco 16 = approved.Total

            return new FormReaderEntries
            {
                Entries = Enumerable.Empty<FormReaderEntry>(),
                Page = page,
                PageSize = pagesize,
                TotalEntries = (int) 0
            };

            //var results = new List<Umbraco.Forms.Core.Persistence.Dtos.Record>();
            //var page = ((int) (approved.Total / ((long)maximum))) + 1;
            //while (results.Count < maximum && page > 0)
            //{
            //    var got = recordReaderService.GetApprovedRecordsFromForm(form, page--, maximum)?.Items?.Where(x => x.State == Umbraco.Forms.Core.Enums.FormState.Approved);
            //    if (got?.Any() == true)
            //    {
            //        results.AddRange(got);
            //    }
            //}

            //return results.OrderByDescending(x => x.Created).Take(maximum)?.Select(x => ToFormReaderEntry(x, nameAlias, defaultName, messageAlias)) ?? Enumerable.Empty<FormReaderEntry>();

        }, cacheLength ?? DefaultCacheLength);
    }
}
