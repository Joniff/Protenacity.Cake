using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorVideoPanelSettings
{
    public EditorVideoRatios RatioTyped => Enum<EditorVideoRatios>.GetValueByDescription(this.Ratio);
    public EditorSubthemes SubthemeTyped => Enum<EditorSubthemes>.GetValueByDescription(this.Subtheme);
    public EditorThemeShades ThemeShadeTyped => Enum<EditorThemeShades>.GetValueByDescription(this.ThemeShade);
    //public EditorNamedIcons PanelIconTyped => Enum<EditorNamedIcons>.GetValueByDescription(this.PanelIcon);
}
