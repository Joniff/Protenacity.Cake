using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Presentation.BusTimeTable;

public class BusTimeTableServicesResponse
{
    public class OperationalDayRecord
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }

    public class ServiceRecord
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("serviceNo")]
        public required string ServiceNo { get; set; }
    }

    public class StopRecord
    {
        [JsonPropertyName("code")]
        public required string Code { get; set; }

        [JsonPropertyName("naptanCode")]
        public required string NaptanCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }
    }

    public class TimeRecord
    {
        [JsonPropertyName("sequence")]
        public required string Sequence { get; set; }

        [JsonPropertyName("time")]
        public required string Time { get; set; }

        [JsonPropertyName("route")]
        public required string Route { get; set; }

        [JsonPropertyName("notes")]
        public required string Notes { get; set; }
    }

    [JsonPropertyName("stop")]
    public required StopRecord Stop { get; set; }

    [JsonPropertyName("services")]
    public required IEnumerable<ServiceRecord> Services { get; set; }
}


