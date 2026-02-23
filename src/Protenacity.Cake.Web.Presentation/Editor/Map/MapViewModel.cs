namespace Protenacity.Cake.Web.Presentation.Editor.Map;

public class MapViewModel
{
    public required Protenacity.Web.OpenStreetMap.Core.Map Map { get; init; }
    public double Ratio { get; init; }
}
