using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorBlockPrimarySettingsBackground2ColorGradient
{
    EditorGradientTypes GradientTypeTyped { get; }
}

public partial class EditorBlockPrimarySettingsBackground2ColorGradient
{
    public EditorGradientTypes GradientTypeTyped => Enum<EditorGradientTypes>.GetValueByDescription(this.GradientType);
}
