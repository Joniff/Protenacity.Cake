using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorOrders
{
    [Description("Default")]
    Default,

    [Description("Latest First")]
    Latest,

    [Description("Oldest First")]
    Oldest,

    [Description("A to Z")]
    AtoZ,

    [Description("Z to A")]
    ZtoA
}

public class EditorOrdersValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorOrders>(dataTypeService)
{

    public override string DataTypeName => "Editor Order";
}