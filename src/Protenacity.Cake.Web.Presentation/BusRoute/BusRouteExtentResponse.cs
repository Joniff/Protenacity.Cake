using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.BusRoute;

public class BusRouteExtentResponse
{
    public class ExtentRecord
    {
        [JsonPropertyName("xmin")]
        public double XMinimum { get; set; }

        [JsonPropertyName("ymin")]
        public double YMinimum { get; set; }

        [JsonPropertyName("xmax")]
        public double XMaximum { get; set; }

        [JsonPropertyName("ymax")]
        public double YMaximum { get; set; }

        public class SpatialReferenceRecord
        {
            [JsonPropertyName("wkid")]
            public int Wkid { get; set; }

            [JsonPropertyName("latestWkid")]
            public int LatestWkid { get; set; }
        }

        [JsonPropertyName("spatialReference")]
        public SpatialReferenceRecord? SpatialReference { get; set; }
    }

    [JsonPropertyName("extent")]
    public ExtentRecord? Extent { get; set; }
}
