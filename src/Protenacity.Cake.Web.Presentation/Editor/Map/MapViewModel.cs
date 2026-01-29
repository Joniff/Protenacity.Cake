using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Map;

public class MapViewModel
{
    public EditorMapTypes MapType { get; init; }
    public required string Id { get; init; }
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
    public required string IconUrl { get; init; }
    public required string Name { get; init; }
    public int Zoom { get; init; }
    public double Ratio { get; init; }
}
