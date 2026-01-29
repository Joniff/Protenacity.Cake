using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorListPrimarySettings
{
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
    public EditorCardStyleImageLocations StyleCardImageLocationTyped => Enum<EditorCardStyleImageLocations>.GetValueByDescription(this.StyleCardImage);
    public EditorCardStyleImageSizes StyleCardImageSizeTyped => Enum<EditorCardStyleImageSizes>.GetValueByDescription(this.StyleCardImageSize);
    public EditorCardStyleHeaders StyleCardHeaderTyped => Enum<EditorCardStyleHeaders>.GetValueByDescription(this.StyleCardHeader);
    public EditorCardStyleDates StyleCardDateTyped => Enum<EditorCardStyleDates>.GetValueByDescription(this.StyleCardDate);
    public EditorCardStyleTimes StyleCardTimeTyped => Enum<EditorCardStyleTimes>.GetValueByDescription(this.StyleCardTime);
    public EditorCardStyleTexts StyleCardTextTyped => Enum<EditorCardStyleTexts>.GetValueByDescription(this.StyleCardText);
    public ActionStyles StyleActionTyped => Enum<ActionStyles>.GetValueByDescription(this.StyleAction);
    public ActionStyleClickAreas StyleActionClickAreaTyped => Enum<ActionStyleClickAreas>.GetValueByDescription(this.StyleActionClickArea);
    public ActionStyleAlignments StyleActionAlignmentTyped => Enum<ActionStyleAlignments>.GetValueByDescription(this.StyleActionAlignment);
    public ListTypes ListTypeTyped => Enum<ListTypes>.GetValueByDescription(this.ListType);
    public EditorSubthemes StyleCardSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.StyleCardSubtheme);
    public EditorThemeShades StyleCardThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.StyleDefaultCardThemeShade);
}
