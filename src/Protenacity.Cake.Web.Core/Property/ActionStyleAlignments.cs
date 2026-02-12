using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionStyleAlignments
{
    [Description("Right Absolute")]
    RightAbsolute = 17,

    [Description("Left Absolute")]
    LeftAbsolute = 18,

    [Description("Centre Absolute")]
    CentreAbsolute = 20,

    [Description("Right Relative")]
    RightRelative = 9,

    [Description("Left Relative")]
    LeftRelative = 10,

    [Description("Centre Relative")]
    CentreRelative = 12,

    // Mappings
    Absolute = 16,
    Relative = 8,
    Right = 1,
    Left = 2,
    Centre = 4
}

public class ActionStyleAlignmentsValueConverter(IDataTypeService dataTypeService) 
    : PropertyValueConverterBase<ActionStyleAlignments>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.DropDownListFlexible;
    public override string DataTypeName => "Editor Card Action Alignment";
}
