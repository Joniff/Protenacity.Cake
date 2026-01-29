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
    EditorSubthemes PageTitleSubthemeTyped { get; }
    EditorThemeShades PageTitleThemeShadeTyped { get; }
    LogoRatios FurnitureLogoRatioTyped { get; }
    double FurnitureLogoRatioCalculated { get; }
    HeaderMenuPositions FurnitureHeaderMenuPositionTyped { get; }
}

public partial class FurnitureTab
{
    public FurntitureStatuses FurnitureStatusTyped => Enum<FurntitureStatuses>.GetValueByDescription(this.FurnitureStatus);
    public BreadcrumbStatuses BreadcrumbStatusTyped => Enum<BreadcrumbStatuses>.GetValueByDescription(this.FurnitureBreadcrumbStatus);
    public EditorSubthemes HeaderSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurnitureHeaderSubtheme);
    public EditorThemeShades HeaderThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurnitureHeaderThemeShade);
    public EditorSubthemes FooterSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurnitureFooterSubtheme);
    public EditorThemeShades FooterThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurnitureFooterThemeShade);
    public EditorSubthemes BreadcrumbSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurnitureBreadcrumbSubtheme);
    public EditorThemeShades BreadcrumbThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurnitureBreadcrumbThemeShade);
    public EditorSubthemes PageTitleSubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.FurniturePageTitleSubtheme);
    public EditorThemeShades PageTitleThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.FurniturePageTitleThemeShade);
    public LogoRatios FurnitureLogoRatioTyped => Enum<LogoRatios>.GetValueByDescription(this.FurnitureLogoRatio);
    public double FurnitureLogoRatioCalculated => ((double)FurnitureLogoRatioTyped) / 360.0;
    public HeaderMenuPositions FurnitureHeaderMenuPositionTyped => Enum<HeaderMenuPositions>.GetValueByDescription(this.FurnitureHeaderMenuPosition);
}