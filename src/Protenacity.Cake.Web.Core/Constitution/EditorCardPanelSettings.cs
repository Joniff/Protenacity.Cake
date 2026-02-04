using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorCardPanelSettings
{
    public EditorCardStyleImageLocations StyleImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleImage);
    public EditorCardStyleImageSizes StyleImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleImageSize);
    public EditorCardStyleHeaders StyleHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleHeader);
    public EditorCardStyleDates StyleDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleDate);
    public EditorCardStyleTimes StyleTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleTime);
    public EditorCardStyleTexts StyleTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleText);
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction);
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.ParseByDescription(this.StyleActionClickArea);
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.ParseByDescription(this.StyleActionAlignment);
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges, EditorBorderEdges.All);
}
