using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorCardEmbeddedSettings
{
    public EditorCardStyleImageLocations StyleImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleImage.ToString()) ?? EditorCardStyleImageLocations.Top;
    public EditorCardStyleImageSizes StyleImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleImageSize.ToString()) ?? EditorCardStyleImageSizes.Medium;
    public EditorCardStyleHeaders StyleHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleHeader.ToString()) ?? EditorCardStyleHeaders.Show;
    public EditorCardStyleDates StyleDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleDate.ToString()) ?? EditorCardStyleDates.Hide;
    public EditorCardStyleTimes StyleTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleTime.ToString()) ?? EditorCardStyleTimes.Hide;
    public EditorCardStyleTexts StyleTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleText.ToString()) ?? EditorCardStyleTexts.Hide;
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction.ToString()) ?? ActionStyles.Hide;
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.ParseByDescription(this.StyleActionClickArea.ToString()) ?? ActionStyleClickAreas.Action;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade.ToString()) ?? EditorThemeShades.Inherit;
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges.ToString(), EditorBorderEdges.All) ?? EditorBorderEdges.All;
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.RightAbsolute;
}
