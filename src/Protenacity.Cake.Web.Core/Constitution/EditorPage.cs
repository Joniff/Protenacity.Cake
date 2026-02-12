using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorPage
{
    public BannerStatuses BannerStatusTyped => BannerStatuses.ParseByDescription(this.BannerStatus.ToString()) ?? BannerStatuses.ShowBanners;
    public AlertStatuses AlertStatusTyped => AlertStatuses.ParseByDescription(this.AlertStatus.ToString()) ?? AlertStatuses.Show;
    public AlertTypes AlertTypeTyped => AlertTypes.ParseByDescription(this.AlertType.ToString()) ?? AlertTypes.Primary;
    public AsideStatuses AsideStatusTyped => AsideStatuses.ParseByDescription(this.AsideStatus.ToString()) ?? AsideStatuses.Hide;
    public FurntitureStatuses FurnitureStatusTyped => FurntitureStatuses.ParseByDescription(this.FurnitureStatus.ToString()) ?? FurntitureStatuses.Inherit;
    public EditorSubthemes HeaderSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureHeaderSubtheme.ToString()) ?? EditorSubthemes.Primary;
    public EditorThemeShades HeaderThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureHeaderThemeShade.ToString()) ?? EditorThemeShades.Inherit;
    public EditorSubthemes FooterSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureFooterSubtheme.ToString()) ?? EditorSubthemes.Primary;
    public EditorThemeShades FooterThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureFooterThemeShade.ToString()) ?? EditorThemeShades.Inherit;
    public BreadcrumbStatuses BreadcrumbStatusTyped => BreadcrumbStatuses.ParseByDescription(this.FurnitureBreadcrumbStatus.ToString()) ?? BreadcrumbStatuses.Inherit;
    public EditorSubthemes BreadcrumbSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureBreadcrumbSubtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades BreadcrumbThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureBreadcrumbThemeShade.ToString()) ?? EditorThemeShades.Inherit;
    public EditorSubthemes PageTitleSubthemeTyped => EditorSubthemes.ParseByDescription(this.PageTitleSubtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades PageTitleThemeShadeTyped => EditorThemeShades.ParseByDescription(this.PageTitleThemeShade.ToString()) ?? EditorThemeShades.Inherit;
    public FurnitureLogoRatios FurnitureLogoRatioTyped => FurnitureLogoRatios.ParseByDescription(this.FurnitureLogoRatio.ToString()) ?? FurnitureLogoRatios.Ratio1x2;
    public double FurnitureLogoRatioCalculated => ((double)FurnitureLogoRatioTyped) / 360.0;
    public HeaderMenuPositions FurnitureHeaderMenuPositionTyped => HeaderMenuPositions.ParseByDescription(this.FurnitureHeaderMenuPosition.ToString()   ) ?? HeaderMenuPositions.InsideRight;
    public SeoStatuses SeoStatusTyped => SeoStatuses.ParseByDescription(this.SeoStatus.ToString()) ?? SeoStatuses.Inherit;
    public SubfooterStatuses SubfooterStatusTyped => SubfooterStatuses.ParseByDescription(this.SubfooterStatus.ToString()   ) ?? SubfooterStatuses.Inherit;
    public EditorThemes ThemeTyped => EditorThemes.ParseByDescription(this.PageTheme.ToString()) ?? EditorThemes.Inherit;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.PageSubtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.PageThemeShade.ToString()) ?? EditorThemeShades.Inherit;
}
