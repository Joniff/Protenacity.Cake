using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorSelectMediaSettings
{
    public EditorCardStyleImageLocations StyleImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleImage.ToString()) ?? EditorCardStyleImageLocations.Top;
    public EditorCardStyleImageSizes StyleImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleImageSize.ToString()) ?? EditorCardStyleImageSizes.Medium;
    public EditorCardStyleHeaders StyleHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleHeader.ToString()) ?? EditorCardStyleHeaders.Show;
    public EditorCardStyleDates StyleDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleDate.ToString()) ?? EditorCardStyleDates.Show;
    public EditorCardStyleTimes StyleTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleTime.ToString()) ?? EditorCardStyleTimes.Show;
    public EditorCardStyleTexts StyleTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleText.ToString()) ?? EditorCardStyleTexts.Show;
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction.ToString()) ?? ActionStyles.Link;
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.ParseByDescription(this.StyleActionClickArea.ToString()) ?? ActionStyleClickAreas.Action ;
    public EditorOrders OrderTyped => EditorOrders.ParseByDescription(this.Order.ToString()) ?? EditorOrders.Default;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme) ?? EditorSubthemes.Primary;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade) ?? EditorThemeShades.Inherit;
    public EditorBorderEdges BorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.BorderEdges, EditorBorderEdges.All) ?? EditorBorderEdges.Top;
    public ActionStyleAlignments StyleActionAlignmentTyped => ActionStyleAlignments.RightAbsolute;
}
