using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor;

public class EditorDefaults
{
    public EditorCardStyleImageLocations CardStyleImageLocation { get; set; }
    public EditorCardStyleImageSizes CardStyleImageSize { get; set; }
    public EditorCardStyleHeaders CardStyleHeader { get; set; }
    public EditorCardStyleDates CardStyleDate { get; set; }
    public EditorCardStyleTimes CardStyleTime { get; set; }
    public EditorCardStyleTexts CardStyleText { get; set; }
    public ActionStyles CardStyleAction { get; set; }
    public ActionStyleClickAreas CardStyleActionClickArea { get; set; }
    public ActionStyleAlignments CardStyleActionAlignment { get; set; }
    public EditorSubthemes CardStyleSubtheme { get; set; }
    public EditorThemeShades CardStyleThemeShade { get; set; }
    public BlockListModel? CardStyleOverrideColor { get; set; }
    public string? CardStyleBorderColor { get; set; }
    public EditorBorderEdges CardStyleBorderEdges { get; set; }
    public EditorTextRestricts TextRestrict { get; set; }
}
