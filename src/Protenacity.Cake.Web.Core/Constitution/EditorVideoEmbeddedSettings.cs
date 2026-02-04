using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorVideoEmbeddedSettings
{
    public EditorVideoRatios RatioTyped => EditorVideoRatios.ParseByDescription(this.Ratio);
    //public EditorTextColors TextColorTyped => EditorTextColors.ParseByDescription(this.TextColor);
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
}
