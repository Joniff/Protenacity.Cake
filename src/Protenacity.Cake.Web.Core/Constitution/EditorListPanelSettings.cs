using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorListPanelSettings
{
    public EditorCardStyleImageLocations StyleCardImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleCardImage);
    public EditorCardStyleImageSizes StyleCardImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleCardImageSize);
    public EditorCardStyleHeaders StyleCardHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleCardHeader);
    public EditorCardStyleDates StyleCardDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleCardDate);
    public EditorCardStyleTimes StyleCardTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleCardTime);
    public EditorCardStyleTexts StyleCardTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleCardText);
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction);
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.ParseByDescription(this.StyleActionClickArea);
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.ParseByDescription(this.StyleActionAlignment);
    public EditorSubthemes StyleCardSubthemeTyped => EditorSubthemes.ParseByDescription(this.StyleCardSubtheme);
    public EditorThemeShades StyleCardThemeShadeTyped => EditorThemeShades.ParseByDescription(this.StyleDefaultCardThemeShade);
}
