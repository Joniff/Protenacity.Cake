using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTabStripPlacements
{
    [Description("Tab strip along Top")]
    Top,

    [Description("Tab strip down Left hand side")]
    Left,

    [Description("Tab strip along Bottom")]
    Bottom,

    [Description("Tap strip down Right hand side")]
    Right
}

public class EditorTabStripPlacementsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorTabStripPlacements>(dataTypeService)
{

    public override string DataTypeName => "Editor Tab Strip Placement";
}
