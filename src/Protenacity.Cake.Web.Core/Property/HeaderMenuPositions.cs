using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum HeaderMenuPositions
{
    [Description("Inside Header Left Justified")]
    InsideLeft,

    [Description("Inside Header Right Justified")]
    InsideRight,

    [Description("Below Header Left Justified")]
    BelowLeft,

    [Description("Below Header Right Justified")]
    BelowRight
}

public class HeaderMenuPositionsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<HeaderMenuPositions>(dataTypeService)
{

    public override string DataTypeName => "Header Menu Position";
}