using System.Globalization;

namespace Protenacity.Cake.Web.Presentation.Coroner.Version1;

public class CoronerInquestBase
{
    public DateTime CalculateDate(string? dateField, string? timeField)
    {
        var culture = new CultureInfo("en-GB");
        if (string.IsNullOrWhiteSpace(dateField))
        {
            return DateTime.MinValue;
        }
        if (string.IsNullOrWhiteSpace(timeField))
        {
            return DateTime.ParseExact(dateField.Trim(), "dd/MM/yyyy", culture);
        }
        return DateTime.TryParseExact(dateField.Trim() + " " + timeField.Trim(), "dd/MM/yyyy hh:mm", culture, DateTimeStyles.AssumeLocal, out var date) ? date : DateTime.MinValue;
    }
}
