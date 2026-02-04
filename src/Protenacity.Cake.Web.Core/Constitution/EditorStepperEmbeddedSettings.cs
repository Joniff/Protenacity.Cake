using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorStepperEmbeddedSettings
{
    EditorStepperOrientation OrientationTyped { get; }
}

public partial class EditorStepperEmbeddedSettings
{
    public EditorStepperOrientation OrientationTyped => EditorStepperOrientation.ParseByDescription(this.Orientation);
}
