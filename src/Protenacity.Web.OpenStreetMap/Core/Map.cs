using Microsoft.AspNetCore.Html;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Encodings.Web;

namespace Protenacity.Web.OpenStreetMap.Core;

[DataContract]
public class Map : IHtmlContent
{
    [DataMember(Name = "zoom", IsRequired = true)]
    public int Zoom { get; set; }

    [DataContract]
    public class Coordinate
    {
        [DataMember(Name = "latitude", IsRequired = true)]
        public decimal Latitude { get; set; }

        [DataMember(Name = "longitude", IsRequired = true)]
        public decimal Longitude { get; set; }
    }

    [DataMember(Name = "marker")]
    public required Coordinate Marker { get; set; } = new Coordinate();

    [DataContract]
    public class BoundingCoordinates
    {
        [DataMember(Name = "northEastCorner", IsRequired = true)]
        public required Coordinate NorthEastCorner { get; set; } = new Coordinate();

        [DataMember(Name = "southWestCorner", IsRequired = true)]
        public required Coordinate SouthWestCorner { get; set; } = new Coordinate();
    }

    [DataMember(Name = "boundingBox", IsRequired = true)]
    public required BoundingCoordinates BoundingBox { get; set; } = new BoundingCoordinates
    {
        NorthEastCorner = new Coordinate(),
        SouthWestCorner = new Coordinate()
    };
    
    public MapConfiguration? Configuration { get; set; }

    public void WriteTo(TextWriter writer, HtmlEncoder encoder)
    {
        if (writer == null)
        {
            throw new ArgumentNullException(nameof(writer));
        }

        if (encoder == null)
        {
            throw new ArgumentNullException(nameof(encoder));
        }

        writer.Write("<iframe width=\"100%\" height=\"400\" frameborder=\"0\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"");
        writer.Write($"https://www.openstreetmap.org/export/embed.html?bbox=");
        writer.Write(BoundingBox.SouthWestCorner.Longitude.ToString(CultureInfo.InvariantCulture));
        writer.Write("%2C");
        writer.Write(BoundingBox.SouthWestCorner.Latitude.ToString(CultureInfo.InvariantCulture));
        writer.Write("%2C");
        writer.Write(BoundingBox.NorthEastCorner.Longitude.ToString(CultureInfo.InvariantCulture));
        writer.Write("%2C");
        writer.Write(BoundingBox.NorthEastCorner.Latitude.ToString(CultureInfo.InvariantCulture));
        writer.Write("&amp;layer=mapnik");
        if (Marker is not null)
        {
            writer.Write("&amp;marker=");
            writer.Write(Marker.Latitude.ToString(CultureInfo.InvariantCulture));
            writer.Write("%2C");
            writer.Write(Marker.Longitude.ToString(CultureInfo.InvariantCulture));
        }
        writer.WriteLine("\" style=\"border: 1px solid black\"></iframe>");
    }
}
