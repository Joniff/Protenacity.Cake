using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionTargets
{
    [Description("_self")]
    CurrentTab,

    [Description("_blank")]
    NewTab
}

//public class ActionTargetsValueConverter(IDataTypeService dataTypeService)
//    : PropertyValueConverterBase<ActionStyleAlignments>(dataTypeService)
//{
//
//    public override string DataTypeName => "--";
//}