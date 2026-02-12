using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorThemes
{
    [Description("Inherit")]
    Inherit,

    [Description("Default")]
    Default,

    [Description("Venice")]
    Venice,

    [Description("Metallic Seaweed")]
    MetallicSeaweed,

    [Description("Deep Dairei")]
    DeepDairei,

    [Description("Cactus Flower")]
    CactusFlower,

    [Description("Mystic Tulip")]
    MysticTulip,

    [Description("Poinciana")]
    Poinciana
}

public class EditorThemesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorThemes>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.RadioButtonList;
    public override string DataTypeName => "Editor Theme Picker";
}