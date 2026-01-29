using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorStepperPanelSettings
{
    public EditorStepperOrientation OrientationTyped => Enum<EditorStepperOrientation>.GetValueByDescription(this.Orientation);
}
