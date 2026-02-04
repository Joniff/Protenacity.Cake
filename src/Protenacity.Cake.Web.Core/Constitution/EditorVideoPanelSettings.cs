using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorVideoPanelSettings
{
    public EditorVideoRatios RatioTyped => EditorVideoRatios.ParseByDescription(this.Ratio);
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade);
    //public EditorNamedIcons PanelIconTyped => EditorNamedIcons.ParseByDescription(this.PanelIcon);
}
