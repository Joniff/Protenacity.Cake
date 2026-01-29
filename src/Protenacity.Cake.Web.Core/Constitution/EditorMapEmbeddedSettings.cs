using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorMapEmbeddedSettings
{
    public EditorMapRatios RatioTyped => Enum<EditorMapRatios>.GetValueByDescription(this.Ratio);
    public double RatioCalculated => ((double)RatioTyped) / 36.0;
    public EditorMapIcons IconTyped => Enum<EditorMapIcons>.GetValueByDescription(this.Icon);
}
