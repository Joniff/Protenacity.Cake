using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionStyleClickAreas
{
    [Description("Link or Button")]
    Action,

    [Description("Card")]
    Card
}

public class ActionStyleClickAreasValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<ActionStyleAlignments>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Card Action Click Area";
}
