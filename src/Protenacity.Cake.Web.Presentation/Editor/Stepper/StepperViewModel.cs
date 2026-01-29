using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Stepper;

public class StepperViewModel
{
    public required string Id { get; init; }
    public EditorStepperOrientation Orientation { get; init; }
    public bool IconCircle { get; init; }

    public class Step
    {
        public string? IconText { get; set; }
        public string? IconUrl { get; set; }
        public IHtmlEncodedString? Header { get; init; }
        public IHtmlEncodedString? Text { get; init; }
        public EditorSubthemes Subtheme { get; init; }
        public EditorThemeShades ThemeShade { get; init; }
        public BlockListModel? OverrideColor { get; init; }
        public string? BorderColor { get; init; }
        public EditorBorderEdges BorderEdges { get; init; }
    }

    public required IEnumerable<Step> Steps { get; init; }
}
