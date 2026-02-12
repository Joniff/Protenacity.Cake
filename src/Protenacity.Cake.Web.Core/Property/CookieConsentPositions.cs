using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum CookieConsentPositions
{
    [Description("Top Left")]
    TopLeft,

    [Description("Top Center")]
    TopCenter,

    [Description("Top Right")]
    TopRight,

    [Description("Middle Left")]
    MiddleLeft,

    [Description("Middle Center")]
    MiddleCenter,

    [Description("Middle Right")]
    MiddleRight,

    [Description("Bottom Left")]
    BottomLeft,

    [Description("Bottom Center")]
    BottomCenter,

    [Description("Bottom Right")]
    BottomRight
}

public class CookieConsentPositionsValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<CookieConsentPositions>(dataTypeService)
{

    public override string DataTypeName => "Cookie Consent Position";
}