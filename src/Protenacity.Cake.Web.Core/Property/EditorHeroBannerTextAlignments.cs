using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorHeroBannerTextAlignments
{
    [Description("Left Align Text")]
    Left,

    [Description("Centre Text")]
    Centre,

    [Description("Right Align Text")]
    Right
}

public class EditorHeroBannerTextAlignmentsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorHeroBannerTextAlignments>(dataTypeService)
{

    public override string DataTypeName => "Editor Hero Banner Text Alignment";
}