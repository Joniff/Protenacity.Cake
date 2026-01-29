using System.ComponentModel.DataAnnotations;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Extensions;

namespace Protenacity.Web.OpenStreetMap.Core;

public class MapRequiredValidator(IJsonSerializer jsonSerializer) : IValueRequiredValidator
{
    public IEnumerable<ValidationResult> ValidateRequired(object? obj, string valueType)
    {
        if (obj == null)
        {
            yield return new ValidationResult("Value cannot be null", new[] {"value"});
            yield break;
        }

        var value = obj.ToString();

        if (string.IsNullOrWhiteSpace(value) || value.DetectIsEmptyJson())
        {
            yield return new ValidationResult("Value cannot be empty", new[] { "value" });
            yield break;
        }

        var model = jsonSerializer.Deserialize<Map>(value);

        if (model?.Marker is null)
        {
            yield return new ValidationResult("The marker has to be set on this map", new[] { "value" });
            yield break;
        }
    }
}