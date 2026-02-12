using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorVideoEmbeddedSettings
{
    public EditorVideoRatios RatioTyped => EditorVideoRatios.ParseByDescription(this.Ratio.ToString()) ?? EditorVideoRatios.SixteenByNine;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade) ?? EditorThemeShades.Inherit;
}
