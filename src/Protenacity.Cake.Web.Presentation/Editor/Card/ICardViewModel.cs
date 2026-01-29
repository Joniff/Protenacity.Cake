using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public interface ICardViewModel : IResponsiveImageViewModel
{
    string Id { get; }
    EditorCardStyleImageLocations ImageStyleLocation { get; }
    EditorCardStyleImageSizes ImageStyleSize { get; }
    EditorCardStyleHeaders HeaderStyle { get; }
    EditorCardStyleDates DateStyle { get; }
    EditorCardStyleTimes TimeStyle { get; }
    IHtmlEncodedString? Header { get; }
    DateTime? Date { get; }
    string? DateFormat { get; }
    string? TimeFormat { get; }
    EditorCardStyleTexts TextStyle { get; }
    IHtmlEncodedString? Text { get; }
    IActionViewModel? Action { get; }
    ActionStyleClickAreas ActionClickArea { get; }
    ActionStyleAlignments ActionAlignment { get; }
    EditorSubthemes Subtheme { get; }
    EditorThemeShades ThemeShade { get; }
    BlockListModel? OverrideColor { get; }
    string? BorderColor { get; }
    EditorBorderEdges BorderEdges { get; }
}
