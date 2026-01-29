using Umbraco.Extensions;
using Umbraco.Forms.Core.Services;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Form;

public class FormReaderService(
    IRecordReaderService recordReaderService, 
    IFusionCache fusionCache) 
    : IFormReaderService
{
    static readonly TimeSpan DefaultCacheLength = new TimeSpan(TimeSpan.TicksPerHour);

    private FormReaderEntry ToFormReaderEntry(Umbraco.Forms.Core.Persistence.Dtos.Record record, string nameAlias, string defaultName, string messageAlias)
    {
        var nameField = record.GetRecordFieldByAlias(nameAlias.ToFirstLowerInvariant()) ?? record.GetRecordField(nameAlias);
        var messageField = record.GetRecordFieldByAlias(messageAlias.ToFirstLowerInvariant()) ?? record.GetRecordField(messageAlias);

        return new FormReaderEntry
        {
            Created = record.Created,
            Name = nameField == null ? "ERROR: Unable to find " + nameAlias + " field in form. This field needs to exist." : nameField?.HasValue() == true ? string.Join(' ', nameField.Values).Trim() : defaultName,
            Message = messageField == null ? "ERROR: Unable to find " + messageAlias + " field in form. This field needs to exist." : messageField?.HasValue() == true ? string.Join(' ', messageField.Values).Trim() : string.Empty
        };
    }

    public FormReaderEntries ApprovedEntriesByDate(Guid form, string nameAlias, string defaultName, string messageAlias, int page = 0, int pagesize = 100, TimeSpan? cacheLength = null)
    {
        var cacheKey = nameof(ApprovedEntriesByDate) + form.ToString("N") + ":" + page.ToString() + ":" + pagesize.ToString();

        return fusionCache.GetOrSet<FormReaderEntries>(cacheKey, _ =>
        {
            var approved = recordReaderService.GetApprovedRecordsFromForm(form, page + 1, pagesize);

            // Umbraco 15 = approved.TotalItems
            // Umbraco 16 = approved.Total

            return new FormReaderEntries
            {
                Entries = approved.Total != 0 ? approved.Items?
                    .Where(x => x.State == Umbraco.Forms.Core.Enums.FormState.Approved)
                    .Select(x => ToFormReaderEntry(x, nameAlias, defaultName, messageAlias))?.OrderByDescending(x => x.Created) ?? Enumerable.Empty<FormReaderEntry>()
                    : Enumerable.Empty<FormReaderEntry>(),
                Page = page,
                PageSize = pagesize,
                TotalEntries = (int) approved.Total
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
