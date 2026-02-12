using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorStepperPrimarySettings
{
    public EditorStepperOrientation OrientationTyped => EditorStepperOrientation.ParseByDescription(this.Orientation.ToString()) ?? EditorStepperOrientation.Vertical;
    public EditorSubthemes SubthemeTyped => EditorSubthemes.ParseByDescription(this.Subtheme.ToString()) ?? EditorSubthemes.Inherit;
    public EditorThemeShades ThemeShadeTyped => EditorThemeShades.ParseByDescription(this.ThemeShade.ToString()) ?? EditorThemeShades.Inherit;
}
