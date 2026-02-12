using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorMapPrimarySettings
{
    public EditorMapRatios RatioTyped => EditorMapRatios.ParseByDescription(this.Ratio.ToString()) ?? EditorMapRatios.Ratio1x2;
    public double RatioCalculated => ((double)RatioTyped) / 36.0;
    public EditorMapIcons IconTyped => EditorMapIcons.ParseByDescription(this.Icon.ToString()) ?? EditorMapIcons.BluePin;
}
