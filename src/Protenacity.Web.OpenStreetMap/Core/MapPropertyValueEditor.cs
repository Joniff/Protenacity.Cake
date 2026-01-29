using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Web.OpenStreetMap.Core;

public class MapPropertyValueEditor: DataValueEditor
{
    private readonly IJsonSerializer _jsonSerializer;
    
    public override IValueRequiredValidator RequiredValidator => new MapRequiredValidator(_jsonSerializer);

    
    public MapPropertyValueEditor(IShortStringHelper shortStringHelper, IJsonSerializer jsonSerializer) : base(shortStringHelper, jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public MapPropertyValueEditor(IShortStringHelper shortStringHelper, IJsonSerializer jsonSerializer, IIOHelper ioHelper, DataEditorAttribute attribute) : base(shortStringHelper, jsonSerializer, ioHelper, attribute)
    {
        _jsonSerializer = jsonSerializer;
    }
}