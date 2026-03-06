using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorThemes
{
    [Description("Inherit")]
    Inherit,

    [Description("Slate")]
    Slate,

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
    Poinciana,

    [Description("Golden Sands")]
    GoldenSands,

    [Description("Myrtle")]
    Myrtle,

    [Description("Candy Floss")]
    CandyFloss,

    [Description("Mana")]
    Mana,

    [Description("Goldfish")]
    Goldfish,

    [Description("Bumblebee")]
    Bumblebee,

    [Description("Avacado")]
    Avacado,
}

public class EditorThemesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorThemes>(dataTypeService)
{
    public override string DataTypeName => "Editor Theme Picker";
}