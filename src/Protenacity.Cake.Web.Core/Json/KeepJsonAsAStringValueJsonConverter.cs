using System.Text.Json;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Core.Json;

public sealed class KeepJsonAsAStringValueJsonConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);

        var element = jsonDoc.RootElement;
        return element.ValueKind == JsonValueKind.String
            ? element.GetString()
            : element.GetRawText();
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.Parse(value);
        jsonDoc.RootElement.WriteTo(writer);
    }
}
