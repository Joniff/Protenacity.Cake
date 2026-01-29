using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public class ResponsiveImage : ResponsiveImageBase, IResponsiveImage
{
    public required MediaWithCrops Image { get; init; }
    public EditorImageCrops ImageCrop { get; init; }
}
