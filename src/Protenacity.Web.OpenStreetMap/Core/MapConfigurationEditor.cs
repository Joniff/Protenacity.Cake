using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Protenacity.Web.OpenStreetMap.Core;

public class MapConfigurationEditor : ConfigurationEditor<MapConfiguration>
{
    public MapConfigurationEditor(IIOHelper ioHelper) : base(ioHelper)
    {
    }
}