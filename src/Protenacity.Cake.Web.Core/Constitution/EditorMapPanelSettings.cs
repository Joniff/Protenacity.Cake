using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorMapPanelSettings
{
    public EditorMapRatios RatioTyped => EditorMapRatios.ParseByDescription(this.Ratio) ?? EditorMapRatios.Ratio1x2;
    public double RatioCalculated => ((double)RatioTyped) / 36.0;
    public EditorMapIcons IconTyped => EditorMapIcons.ParseByDescription(this.Icon) ?? EditorMapIcons.BluePin;
}
