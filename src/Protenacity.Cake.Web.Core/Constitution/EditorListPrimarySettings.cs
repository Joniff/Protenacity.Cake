using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorListPrimarySettings
{
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    public EditorCardStyleImageLocations StyleCardImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleCardImage);
    public EditorCardStyleImageSizes StyleCardImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleCardImageSize);
    public EditorCardStyleHeaders StyleCardHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleCardHeader);
    public EditorCardStyleDates StyleCardDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleCardDate);
    public EditorCardStyleTimes StyleCardTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleCardTime);
    public EditorCardStyleTexts StyleCardTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleCardText);
    public ActionStyles StyleActionTyped => ActionStyles.Button;
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.Action;
    public ListTypes ListTypeTyped => ListTypes.ParseByDescription(this.ListType);
    public EditorSubthemes StyleCardSubthemeTyped => EditorSubthemes.ParseByDescription(this.StyleCardSubtheme);
    public EditorThemeShades StyleCardThemeShadeTyped => EditorThemeShades.ParseByDescription(this.StyleDefaultCardThemeShade);
}
