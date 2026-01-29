using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.OpeningTimes;

public class OpeningTimesViewModel
{
    public required string Id { get; init; }
    public IHtmlEncodedString? Header { get; init; }
    public IHtmlEncodedString? Text { get; init; }
    public IHtmlEncodedString? Footer { get; init; }
    public EditorDayOfWeek Today { get; init; }

    public required IEnumerable<EditorDayOfWeek> DayOrder { get; init; }

    public class Timeslot
    {
        public EditorDayOfWeek Day { get; init; }
        public IHtmlEncodedString? Text { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
    }

    public required IDictionary<EditorDayOfWeek, IEnumerable<Timeslot>> Timeslots { get; init; }

    public IEnumerable<Timeslot> GetTimeslotsForDay(EditorDayOfWeek day) => Timeslots.TryGetValue(day, out var timeslots) ? timeslots : Enumerable.Empty<Timeslot>();
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
    public required string CustomTimeFormat { get; init; }
}
