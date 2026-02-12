using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorPage
{
    public BannerStatuses BannerStatusTyped => BannerStatuses.ParseByDescription(this.BannerStatus) ?? BannerStatuses.ShowBanners;
    public AlertStatuses AlertStatusTyped => AlertStatuses.ParseByDescription(this.AlertStatus) ?? AlertStatuses.Show;
    public AlertTypes AlertTypeTyped => AlertTypes.ParseByDescription(this.AlertType.ToString()) ?? AlertTypes.Primary;
    public AsideStatuses AsideStatusTyped => AsideStatuses.ParseByDescription(this.AsideStatus) ?? AsideStatuses.Hide;
    public FurntitureStatuses FurnitureStatusTyped => FurntitureStatuses.ParseByDescription(this.FurnitureStatus) ?? FurntitureStatuses.Inherit;
    public EditorSubthemes HeaderSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureHeaderSubtheme) ?? EditorSubthemes.Primary;
    public EditorThemeShades HeaderThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureHeaderThemeShade) ?? EditorThemeShades.Inherit;
    public EditorSubthemes FooterSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureFooterSubtheme) ?? EditorSubthemes.Primary;
    public EditorThemeShades FooterThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureFooterThemeShade) ?? EditorThemeShades.Inherit;
    public BreadcrumbStatuses BreadcrumbStatusTyped => BreadcrumbStatuses.ParseByDescription(this.FurnitureBreadcrumbStatus) ?? BreadcrumbStatuses.Inherit;
    public EditorSubthemes BreadcrumbSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureBreadcrumbSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades BreadcrumbThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureBreadcrumbThemeShade) ?? EditorThemeShades.Inherit;
    public EditorSubthemes PageTitleSubthemeTyped => EditorSubthemes.ParseByDescription(this.PageTitleSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades PageTitleThemeShadeTyped => EditorThemeShades.ParseByDescription(this.PageTitleThemeShade) ?? EditorThemeShades.Inherit;
    public FurnitureLogoRatios FurnitureLogoRatioTyped => FurnitureLogoRatios.ParseByDescription(this.FurnitureLogoRatio.ToString()) ?? FurnitureLogoRatios.Ratio1x2;
    public double FurnitureLogoRatioCalculated => ((double)FurnitureLogoRatioTyped) / 360.0;
    public HeaderMenuPositions FurnitureHeaderMenuPositionTyped => HeaderMenuPositions.ParseByDescription(this.FurnitureHeaderMenuPosition) ?? HeaderMenuPositions.InsideRight;
    public SeoStatuses SeoStatusTyped => SeoStatuses.ParseByDescription(this.SeoStatus) ?? SeoStatuses.Inherit;
    public SubfooterStatuses SubfooterStatusTyped => SubfooterStatuses.ParseByDescription(this.SubfooterStatus) ?? SubfooterStatuses.Inherit;
    public EditorThemes ThemeTyped => EditorThemes.ParseByDescription(this.PageTheme) ?? EditorThemes.Inherit;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.PageSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.PageThemeShade) ?? EditorThemeShades.Inherit;
}
