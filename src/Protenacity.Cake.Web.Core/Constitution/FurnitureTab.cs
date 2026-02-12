using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IFurnitureTab
{
    FurntitureStatuses FurnitureStatusTyped { get; }
    BreadcrumbStatuses BreadcrumbStatusTyped { get; }
    EditorSubthemes HeaderSubthemeTyped { get; }
    EditorThemeShades HeaderThemeShadeTyped { get; }
    EditorSubthemes FooterSubthemeTyped { get; }
    EditorThemeShades FooterThemeShadeTyped { get; }
    EditorSubthemes BreadcrumbSubthemeTyped { get; }
    EditorThemeShades BreadcrumbThemeShadeTyped { get; }
    FurnitureLogoRatios FurnitureLogoRatioTyped { get; }
    double FurnitureLogoRatioCalculated { get; }
    HeaderMenuPositions FurnitureHeaderMenuPositionTyped { get; }
}

public partial class FurnitureTab
{
    public FurntitureStatuses FurnitureStatusTyped => FurntitureStatuses.ParseByDescription(this.FurnitureStatus) ?? FurntitureStatuses.Inherit;
    public BreadcrumbStatuses BreadcrumbStatusTyped => BreadcrumbStatuses.ParseByDescription(this.FurnitureBreadcrumbStatus) ?? BreadcrumbStatuses.Inherit;
    public EditorSubthemes HeaderSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureHeaderSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades HeaderThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureHeaderThemeShade) ?? EditorThemeShades.Inherit;
    public EditorSubthemes FooterSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureFooterSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades FooterThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureFooterThemeShade) ?? EditorThemeShades.Inherit;
    public EditorSubthemes BreadcrumbSubthemeTyped => EditorSubthemes.ParseByDescription(this.FurnitureBreadcrumbSubtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades BreadcrumbThemeShadeTyped => EditorThemeShades.ParseByDescription(this.FurnitureBreadcrumbThemeShade) ?? EditorThemeShades.Inherit;
    public FurnitureLogoRatios FurnitureLogoRatioTyped => FurnitureLogoRatios.ParseByDescription(this.FurnitureLogoRatio.ToString()) ?? FurnitureLogoRatios.Ratio1x2;
    public double FurnitureLogoRatioCalculated => ((double)FurnitureLogoRatioTyped) / 360.0;
    public HeaderMenuPositions FurnitureHeaderMenuPositionTyped => HeaderMenuPositions.ParseByDescription(this.FurnitureHeaderMenuPosition) ?? HeaderMenuPositions.InsideRight;
}