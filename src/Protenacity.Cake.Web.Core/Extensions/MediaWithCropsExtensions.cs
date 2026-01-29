using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class MediaWithCropsExtensions
{
    public static string? GetCropUrl(this MediaWithCrops mediaWithCrops, EditorImageCrops crop, UrlMode urlMode = UrlMode.Default)
    {
        return mediaWithCrops.GetCropUrl(Enum<EditorImageCrops>.GetDescriptionByValue(crop), urlMode);
    }

    public static string? AlternateText(this MediaWithCrops mediaWithCrops, string? defaultValue = null)
    {
        var editorImage = mediaWithCrops.Content as EditorImage;
        if (!string.IsNullOrWhiteSpace(editorImage?.AlternateText))
        {
            return editorImage?.AlternateText;
        }

        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            return defaultValue;
        }

        return "";
    }
}
