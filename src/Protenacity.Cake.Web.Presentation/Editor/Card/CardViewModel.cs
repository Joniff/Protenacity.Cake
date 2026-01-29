using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class CardViewModel : ResponsiveImageViewModel, ICardViewModel
{
    public required string Id { get; init; }
    public EditorCardStyleImageLocations ImageStyleLocation { get; init; }
    public EditorCardStyleImageSizes ImageStyleSize { get; init; }
    public EditorCardStyleHeaders HeaderStyle { get; init; }
    public EditorCardStyleDates DateStyle { get; init; }
    public EditorCardStyleTimes TimeStyle { get; init; }
    public IHtmlEncodedString? Header { get; init; }
    public DateTime? Date { get; init; }
    public string? DateFormat { get; init; }
    public string? TimeFormat { get; init; }
    public EditorCardStyleTexts TextStyle { get; init; }
    public IHtmlEncodedString? Text { get; init; }
    public IActionViewModel? Action { get; init; }
    public ActionStyleClickAreas ActionClickArea { get; init; }
    public ActionStyleAlignments ActionAlignment { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public string? BorderColor { get; init; }
    public EditorBorderEdges BorderEdges { get; init; }
}
