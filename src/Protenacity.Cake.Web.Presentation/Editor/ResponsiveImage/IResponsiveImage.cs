using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public interface IResponsiveImage : IResponsiveImageBase
{
    MediaWithCrops Image { get; }
    EditorImageCrops ImageCrop { get; }
}
