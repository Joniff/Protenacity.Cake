using System.ComponentModel;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum BannerStatuses
{
    [Description("Inherit")]
    Inherit,

    [Description("Show Hero Banners")]
    ShowBanners,

    [Description("Show Single Image")]
    ShowImage,

    [Description("Hide")]
    Hide
}

public class BannerStatusesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<BannerStatuses>(dataTypeService)
{
    public override string PropertyTypeName => Constants.PropertyEditors.Aliases.RadioButtonList;
    public override string DataTypeName => "Banner Status";
}