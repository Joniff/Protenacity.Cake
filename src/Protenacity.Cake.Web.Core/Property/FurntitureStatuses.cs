using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum FurntitureStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Show")]
    Show
}

public class FurntitureStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<FurntitureStatuses>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Furniture Status";
}