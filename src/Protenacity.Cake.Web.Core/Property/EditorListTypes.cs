using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorListTypes
{
    [Description("Flexible Grid")]
    Grid,

    [Description("Carousel")]
    Carousel,
}

public class EditorListTypesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorListTypes>(dataTypeService)
{

    public override string DataTypeName => "Editor List Type";
}