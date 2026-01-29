using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorBlockPrimarySettingsBackground3ColorGradient
{
    public EditorGradientTypes GradientTypeTyped => Enum<EditorGradientTypes>.GetValueByDescription(this.GradientType);

}
