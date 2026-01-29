using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;

namespace Protenacity.Web.OpenStreetMap.Core;

public class MapPropertyValueConverter(IJsonSerializer jsonSerializer) : PropertyValueConverterBase
{
    public override bool IsConverter(IPublishedPropertyType propertyType) => propertyType.EditorAlias.Equals(MapPropertyEditor.EditorAlias);
    
    public override Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(Map);
    
    public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) => PropertyCacheLevel.Element;
    
    public override object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview)
    {
        var configuration = propertyType.DataType.ConfigurationAs<MapConfiguration>();
        if (configuration == null)
        {
            return null;
        }

        var interString = inter?.ToString();
        var model = (string.IsNullOrWhiteSpace(interString) ? configuration?.DefaultPosition : jsonSerializer.Deserialize<Map>(interString));
        if (model == null)
        {
            return null;
        }

        model.Configuration = configuration!;
        return model;
    }
}