using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorPage
{
    public BannerStatuses BannerStatusTyped => Enum<BannerStatuses>.GetValueByDescription(this.BannerStatus);
    public AlertStatuses AlertStatusTyped => Enum<AlertStatuses>.GetValueByDescription(this.AlertStatus);
    public AlertTypes AlertTypeTyped => Enum<AlertTypes>.GetValueByDescription(this.AlertType);
    public AsideStatuses AsideStatusTyped => Enum<AsideStatuses>.GetValueByDescription(this.AsideStatus);
    public FurntitureStatuses FurnitureStatusTyped => Enum<FurntitureStatuses>.GetValueByDescription(this.FurnitureStatus);
    public EditorSubthemes HeaderSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurnitureHeaderSubtheme);
    public EditorThemeShades HeaderThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurnitureHeaderThemeShade);
    public EditorSubthemes FooterSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurnitureFooterSubtheme);
    public EditorThemeShades FooterThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurnitureFooterThemeShade);
    public BreadcrumbStatuses BreadcrumbStatusTyped => Enum<BreadcrumbStatuses>.GetValueByDescription(this.FurnitureBreadcrumbStatus);
    public EditorSubthemes BreadcrumbSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurnitureBreadcrumbSubtheme);
    public EditorThemeShades BreadcrumbThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurnitureBreadcrumbThemeShade);
    public EditorSubthemes PageTitleSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurniturePageTitleSubtheme);
    public EditorThemeShades PageTitleThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurniturePageTitleThemeShade);
    public LogoRatios FurnitureLogoRatioTyped => Enum<LogoRatios>.GetValueByDescription(this.FurnitureLogoRatio);
    public double FurnitureLogoRatioCalculated => ((double)FurnitureLogoRatioTyped) / 360.0;
    public HeaderMenuPositions FurnitureHeaderMenuPositionTyped => Enum<HeaderMenuPositions>.GetValueByDescription(this.FurnitureHeaderMenuPosition);
    public SeoStatuses SeoStatusTyped => Enum<SeoStatuses>.GetValueByDescription(this.SeoStatus);
    public ReviewStatuses ReviewStatusTyped => Enum<ReviewStatuses>.GetValueByDescription(this.ReviewStatus);
    public SubfooterStatuses SubfooterStatusTyped => Enum<SubfooterStatuses>.GetValueByDescription(this.SubfooterStatus);
    public EditorThemes ThemeTyped => Enum<EditorThemes>.GetValueByDescription(this.PageTheme);
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.PageSubtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.PageThemeShade);
    public VirtualAgentStatuses VirtualAgentStatusTyped => Enum<VirtualAgentStatuses>.GetValueByDescription(this.VirtualAgentStatus);
}
