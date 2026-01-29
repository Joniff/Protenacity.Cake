using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.View;

public interface IResponsiveImageService
{
    IEnumerable<Tuple<int?, string>> ImageUrls(MediaWithCrops? media, EditorImageCrops crop, int widthFactor, int quality);
}
