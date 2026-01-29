using System.Text.Json;
using System.Text.Json.Serialization;

namespace Protenacity.Cake.Web.Core.Json;

public class SerializeInterfaceOnly<T> : JsonConverter<T>
{
    public override T Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(
        Utf8JsonWriter writer,
        T value,
        JsonSerializerOptions options)
    {
        if (value is null)
        {
            JsonSerializer.Serialize(writer, default(T), options);
        }
        else
        {
            foreach (var prop in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
            {
                JsonSerializer.Serialize(writer, prop.GetValue(value), prop.PropertyType, options);
            }
        }
    }
}
