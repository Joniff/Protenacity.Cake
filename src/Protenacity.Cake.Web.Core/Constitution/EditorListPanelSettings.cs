using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorListPanelSettings
{
    public EditorCardStyleImageLocations StyleCardImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleCardImage.ToString()) ?? EditorCardStyleImageLocations.Top;
    public EditorCardStyleImageSizes StyleCardImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleCardImageSize.ToString()) ?? EditorCardStyleImageSizes.Medium;
    public EditorCardStyleHeaders StyleCardHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleCardHeader.ToString()) ?? EditorCardStyleHeaders.Show;
    public EditorCardStyleDates StyleCardDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleCardDate.ToString()) ?? EditorCardStyleDates.Show;
    public EditorCardStyleTimes StyleCardTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleCardTime.ToString()) ?? EditorCardStyleTimes.Show;
    public EditorCardStyleTexts StyleCardTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleCardText.ToString()) ?? EditorCardStyleTexts.Show;
    public ActionStyles StyleActionTyped => ActionStyles.Button;
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.Action;
    public EditorSubthemes StyleCardSubthemeTyped => EditorSubthemes.ParseByDescription(this.StyleCardSubtheme) ?? EditorSubthemes.Primary;
    public EditorThemeShades StyleCardThemeShadeTyped => EditorThemeShades.ParseByDescription(this.StyleDefaultCardThemeShade) ?? EditorThemeShades.Light;
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.RightAbsolute; // QWERTY
}
