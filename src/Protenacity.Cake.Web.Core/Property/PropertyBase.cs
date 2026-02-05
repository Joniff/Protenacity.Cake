using Protenacity.Cake.Web.Core.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public abstract class PropertyValueConverterBase<E>(IDataTypeService dataTypeService) : IPropertyValueConverter where E : Enum
{
    public abstract string PropertyTypeName { get; }
    public abstract string DataTypeName { get; }

    public bool IsConverter(IPublishedPropertyType propertyType)
    {
        if (!propertyType.IsUserProperty || propertyType.DataType.EditorAlias != PropertyTypeName)
        {
            return false;
        }

        var dataType = dataTypeService.GetByEditorUiAlias(propertyType.DataType.EditorUiAlias).Result.FirstOrDefault(d => d.Id == propertyType.DataType.Id);

        if (dataType?.Name != DataTypeName)
        {
            return false;
        }

        //   Check to see if values set in DataType match our Enum

        var items = dataType.ConfigurationObject as IEnumerable<string>;

        if (items?.Any() != true)
        {
            return false;
        }

        foreach (var value in items)
        {
            if (EnumExtensions.ParseByDescription<E>(value, true, default(E)) == null)
            {
                throw new ArgumentOutOfRangeException("Value " + nameof(value) + " is a valid value for " + PropertyTypeName + " Data Type, but doesn\'t exist in corresponding " + typeof(E).Name + " Enum");
            }
        }

        return true;
    }

    public object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview)
        => EnumExtensions.ParseByDescription<E>(inter?.ToString(), true, default(E));

    public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
    {
        throw new NotImplementedException();
    }

    public object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview) 
        => EnumExtensions.ParseByDescription<E>(source?.ToString(), true, default(E));

    public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) => PropertyCacheLevel.Element;
    public bool? IsValue(object? value, PropertyValueLevel level) => Enum.TryParse<ActionStyleAlignments>(value?.ToString(), out _);

    Type IPropertyValueConverter.GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(E);

}