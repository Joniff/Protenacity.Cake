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
    public EditorCardStyleImageLocations StyleCardImageLocationTyped => Enum<EditorCardStyleImageLocations>.GetValueByDescription(this.StyleCardImage);
    public EditorCardStyleImageSizes StyleCardImageSizeTyped => Enum<EditorCardStyleImageSizes>.GetValueByDescription(this.StyleCardImageSize);
    public EditorCardStyleHeaders StyleCardHeaderTyped => Enum<EditorCardStyleHeaders>.GetValueByDescription(this.StyleCardHeader);
    public EditorCardStyleDates StyleCardDateTyped => Enum<EditorCardStyleDates>.GetValueByDescription(this.StyleCardDate);
    public EditorCardStyleTimes StyleCardTimeTyped => Enum<EditorCardStyleTimes>.GetValueByDescription(this.StyleCardTime);
    public EditorCardStyleTexts StyleCardTextTyped => Enum<EditorCardStyleTexts>.GetValueByDescription(this.StyleCardText);
    public EditorSubthemes StyleCardSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.StyleCardSubtheme);
    public EditorThemeShades StyleCardThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.StyleDefaultCardThemeShade);
}
