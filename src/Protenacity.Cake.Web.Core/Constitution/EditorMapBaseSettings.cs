using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorMapBaseSettings
{
    EditorMapRatios RatioTyped { get; }
    double RatioCalculated { get; }
    EditorMapIcons IconTyped { get; }
}

public partial class EditorMapBaseSettings
{
    public EditorMapRatios RatioTyped => Enum<EditorMapRatios>.GetValueByDescription(this.Ratio);
    public double RatioCalculated => ((double)RatioTyped) / 36.0;
    public EditorMapIcons IconTyped => Enum<EditorMapIcons>.GetValueByDescription(this.Icon);
}
