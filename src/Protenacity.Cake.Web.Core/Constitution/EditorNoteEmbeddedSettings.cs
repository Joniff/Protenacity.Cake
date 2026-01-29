using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorNoteEmbeddedSettings
{
    public EditorCardStyleImageLocations StyleImageLocationTyped => Enum<EditorCardStyleImageLocations>.GetValueByDescription(this.StyleImage);
    public EditorCardStyleImageSizes StyleImageSizeTyped => Enum<EditorCardStyleImageSizes>.GetValueByDescription(this.StyleImageSize);
    public EditorCardStyleHeaders StyleHeaderTyped => Enum<EditorCardStyleHeaders>.GetValueByDescription(this.StyleHeader);
    public EditorCardStyleDates StyleDateTyped => Enum<EditorCardStyleDates>.GetValueByDescription(this.StyleDate);
    public EditorCardStyleTimes StyleTimeTyped => Enum<EditorCardStyleTimes>.GetValueByDescription(this.StyleTime);
    public EditorCardStyleTexts StyleTextTyped => Enum<EditorCardStyleTexts>.GetValueByDescription(this.StyleText);
    public ActionStyles StyleActionTyped => Enum<ActionStyles>.GetValueByDescription(this.StyleAction);
    public ActionStyleClickAreas StyleActionClickAreaTyped => Enum<ActionStyleClickAreas>.GetValueByDescription(this.StyleActionClickArea);
    public ActionStyleAlignments StyleActionAlignmentTyped => Enum<ActionStyleAlignments>.GetValueByDescription(this.StyleActionAlignment);
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
    public EditorBorderEdges BorderEdgesTyped => Enum<EditorBorderEdges>.GetValueByDescription(this.BorderEdges, EditorBorderEdges.All);
}
