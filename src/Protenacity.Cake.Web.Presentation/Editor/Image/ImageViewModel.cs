using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.Image;

public class ImageViewModel
{
    public required MediaWithCrops Image { get; init; }
    public bool FullScreen { get; init; }

}
