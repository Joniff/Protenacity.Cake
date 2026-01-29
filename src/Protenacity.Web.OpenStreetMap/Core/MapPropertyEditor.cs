using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;

namespace Protenacity.Web.OpenStreetMap.Core;

[DataEditor(EditorAlias, ValueType = ValueTypes.Json)]
public class MapPropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper ioHelper)
    : DataEditor(dataValueEditorFactory)
{
    public const string EditorAlias = "Protenacity.Web.OpenStreetMap";

    /// <inheritdoc />
    protected override IDataValueEditor CreateValueEditor() => Attribute == null ? DataValueEditorFactory.Create<MapPropertyValueEditor>() : DataValueEditorFactory.Create<MapPropertyValueEditor>(Attribute);
    
    /// <inheritdoc />
    protected override IConfigurationEditor CreateConfigurationEditor() => new MapConfigurationEditor(ioHelper);
}