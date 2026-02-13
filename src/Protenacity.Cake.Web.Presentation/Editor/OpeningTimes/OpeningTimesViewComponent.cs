using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Protenacity.Cake.Web.Presentation.Editor.OpeningTimes;

public class OpeningTimesViewComponent(IViewService viewService) : ThemeViewComponent
{
    public const string Name = nameof(OpeningTimes);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";
    private static CultureInfo culture = new CultureInfo("en-GB");

    private DateTime? Time24(string? text)
    {
        return DateTime.TryParseExact(text, "HH:mm", culture, DateTimeStyles.None, out var time) ? time : null;
    }

    public IViewComponentResult Invoke(IEditorContent content)
    {
        IEditorOpeningTimesEmbedded openingTimes = content.Block?.Content as IEditorOpeningTimesEmbedded ??
            throw new ApplicationException("Needs to be " + nameof(IEditorOpeningTimesEmbedded));
        var slots = new List<OpeningTimesViewModel.Timeslot>();
        var editorTimeslots = openingTimes.Times?.Select(t => (EditorTimeslot) t.Content);

        if (editorTimeslots?.Any() == true)
        {
            foreach (var editorTimeslot in editorTimeslots)
            {
                var start = Time24(editorTimeslot.Open);
                var end = Time24(editorTimeslot.Close);

                if (start != null && end != null)
                {
                    slots.Add(new OpeningTimesViewModel.Timeslot
                    {
                        Day = editorTimeslot.Day,
                        Text = editorTimeslot.Text,
                        Start = start ?? DateTime.MinValue,
                        End = end ?? DateTime.MinValue
                    });
                }
            }
        }

        return View(new OpeningTimesViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Header = openingTimes?.Header,
            Text = openingTimes?.Text,
            Footer = openingTimes?.Footer,
            Today = (EditorDayOfWeek) DateTime.UtcNow.DayOfWeek,
            Timeslots = slots.GroupBy(s => s.Day).ToDictionary(slots => slots.Key, slots => slots.AsEnumerable()),
            DayOrder = new EditorDayOfWeek[] { EditorDayOfWeek.Monday, EditorDayOfWeek.Tuesday, EditorDayOfWeek.Wednesday, EditorDayOfWeek.Thursday, EditorDayOfWeek.Friday, EditorDayOfWeek.Saturday, EditorDayOfWeek.Sunday },
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content),
            CustomTimeFormat = viewService.CurrentDomainPage.ConfigOpeningTimesCustomTimeFormat ?? "T"
        });
    }
}
