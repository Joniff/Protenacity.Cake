using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorCardDefaultSettings
{
    EditorCardStyleImageLocations StyleCardImageLocationTyped { get; }
    EditorCardStyleImageSizes StyleCardImageSizeTyped { get; }
    EditorCardStyleHeaders StyleCardHeaderTyped { get; }
    EditorCardStyleDates StyleCardDateTyped { get; }
    EditorCardStyleTimes StyleCardTimeTyped { get; }
    EditorCardStyleTexts StyleCardTextTyped { get; }
    EditorSubthemes StyleCardSubthemeTyped { get; }
    EditorThemeShades StyleCardThemeShadeTyped { get; }
}


public partial class EditorCardDefaultSettings
{
    public EditorCardStyleImageLocations StyleCardImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleCardImage) ?? EditorCardStyleImageLocations.Top;
    public EditorCardStyleImageSizes StyleCardImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleCardImageSize) ?? EditorCardStyleImageSizes.Medium;
    public EditorCardStyleHeaders StyleCardHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleCardHeader) ?? EditorCardStyleHeaders.Show;
    public EditorCardStyleDates StyleCardDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleCardDate) ?? EditorCardStyleDates.Show;
    public EditorCardStyleTimes StyleCardTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleCardTime) ?? EditorCardStyleTimes.Show;
    public EditorCardStyleTexts StyleCardTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleCardText) ?? EditorCardStyleTexts.Show;
    public EditorSubthemes StyleCardSubthemeTyped => EditorSubthemes.ParseByDescription(this.StyleCardSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades StyleCardThemeShadeTyped => EditorThemeShades.ParseByDescription(this.StyleDefaultCardThemeShade) ?? EditorThemeShades.Inherit;
}
