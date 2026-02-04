using Protenacity.Cake.Web.Core.Extensions;
using System.ComponentModel;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionStyleAlignments
{
    //   Put Right at top so that it is default
    [Description("Right Absolute")]
    RightAbsolute = 17,

    [Description("Left Absolute")]
    LeftAbsolute = 18,

    [Description("Centre Absolute")]
    CentreAbsolute = 20,

    [Description("Right Relative")]
    RightRelative = 9,

    [Description("Left Relative")]
    LeftRelative = 10,

    [Description("Centre Relative")]
    CentreRelative = 12,

    // Mappings
    Absolute = 16,
    Relative = 8,
    Right = 1,
    Left = 2,
    Centre = 4
}

public class ActionStyleAlignmentsValueConverter : IPropertyValueConverter
{
    public bool IsConverter(IPublishedPropertyType propertyType) => propertyType.EditorUiAlias == "UmbracoDayOfWeek";
    public object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview) 
        => ActionStyleAlignments.ParseByDescription(inter?.ToString());

    public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
    {
        throw new NotImplementedException();
    }

    public object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview) => Enum.TryParse<DayOfWeek>(source?.ToString(), out DayOfWeek d) ? d : null;

    public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) => PropertyCacheLevel.Element;
    public bool? IsValue(object? value, PropertyValueLevel level) => Enum.TryParse<DayOfWeek>(value?.ToString(), out _);

    Type IPropertyValueConverter.GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(DayOfWeek);

}
