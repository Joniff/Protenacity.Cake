using System.Globalization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version2;

public class CoronerInquestBase
{
    public DateTime CalculateDate(string? dateField, string? timeField)
    {
        var culture = new CultureInfo("en-GB");
        if (string.IsNullOrWhiteSpace(dateField))
        {
            return DateTime.MinValue;
        }
        if (DateTime.TryParseExact(dateField.Trim(), "dd/MM/yyyy HH:mm", culture, DateTimeStyles.AssumeLocal, out var date1))
        {
            return date1;
        }
        if (!string.IsNullOrWhiteSpace(timeField) && DateTime.TryParseExact(dateField.Trim() + " " + timeField.Trim(), "dd/MM/yyyy HH:mm", culture, DateTimeStyles.AssumeLocal, out var date2))
        {
            return date2;
        }
        return DateTime.TryParseExact(dateField.Trim(), "dd/MM/yyyy", culture, DateTimeStyles.AssumeLocal, out var date3) ? date3 : DateTime.MinValue;
    }
}
