using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorPage
{
    public BannerStatuses BannerStatusTyped => BannerStatuses.ParseByDescription(this.BannerStatus);
    public AlertStatuses AlertStatusTyped => AlertStatuses.ParseByDescription(this.AlertStatus);
    public AlertTypes AlertTypeTyped => AlertTypes.ParseByDescription(this.AlertType);
    public AsideStatuses AsideStatusTyped => AsideStatuses.ParseByDescription(this.AsideStatus);
    public FurntitureStatuses FurnitureStatusTyped => FurntitureStatuses.ParseByDescription(this.FurnitureStatus);
    public EditorSubthemes HeaderSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureHeaderSubtheme);
    public EditorThemeShades HeaderThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureHeaderThemeShade);
    public EditorSubthemes FooterSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureFooterSubtheme);
    public EditorThemeShades FooterThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureFooterThemeShade);
    public BreadcrumbStatuses BreadcrumbStatusTyped => BreadcrumbStatuses.ParseByDescription(this.FurnitureBreadcrumbStatus);
    public EditorSubthemes BreadcrumbSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureBreadcrumbSubtheme);
    public EditorThemeShades BreadcrumbThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureBreadcrumbThemeShade);
    public EditorSubthemes PageTitleSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurniturePageTitleSubtheme);
    public EditorThemeShades PageTitleThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurniturePageTitleThemeShade);
    public LogoRatios FurnitureLogoRatioTyped => LogoRatios.ParseByDescription(this.FurnitureLogoRatio);
    public double FurnitureLogoRatioCalculated => ((double)FurnitureLogoRatioTyped) / 360.0;
    public HeaderMenuPositions FurnitureHeaderMenuPositionTyped => HeaderMenuPositions.ParseByDescription(this.FurnitureHeaderMenuPosition);
    public SeoStatuses SeoStatusTyped => SeoStatuses.ParseByDescription(this.SeoStatus);
    public ReviewStatuses ReviewStatusTyped => ReviewStatuses.ParseByDescription(this.ReviewStatus);
    public SubfooterStatuses SubfooterStatusTyped => SubfooterStatuses.ParseByDescription(this.SubfooterStatus);
    public EditorThemes ThemeTyped => EditorThemes.ParseByDescription(this.PageTheme);
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.PageSubtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.PageThemeShade);
    public VirtualAgentStatuses VirtualAgentStatusTyped => VirtualAgentStatuses.ParseByDescription(this.VirtualAgentStatus);
}
