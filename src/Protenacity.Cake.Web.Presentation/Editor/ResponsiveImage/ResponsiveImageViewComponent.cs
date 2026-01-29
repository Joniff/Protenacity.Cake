using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public class ResponsiveImageViewComponent(IResponsiveImageService responsiveImageService, IViewService viewService)
    : ViewComponent
{
    public const string Name = nameof(ResponsiveImage);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(ResponsiveImageBase model)
    {
        if (model.GetType().IsAssignableTo(typeof(IResponsiveImageViewModel)))
        {
            var vm = (IResponsiveImageViewModel)model;
            return vm.Urls == null ? Content(string.Empty) : View(vm);
        }
        else if (model.GetType().IsAssignableTo(typeof(IResponsiveImage)))
        {
            var responsiveImage = (IResponsiveImage)model;
            return View(new ResponsiveImageViewModel
            {
                AlternateTitle = responsiveImage.AlternateTitle,
                BackgroundId = responsiveImage.BackgroundId,
                Opacity = responsiveImage.Opacity,
                Urls = responsiveImageService.ImageUrls(responsiveImage.Image, responsiveImage.ImageCrop, responsiveImage.WidthFactorImage, responsiveImage.ImageQuality ?? viewService.CurrentDomainPage.ConfigImageQuality),
                WidthFactorImage = responsiveImage.WidthFactorImage,
                WidthFactorContainer = responsiveImage.WidthFactorContainer
            });
        }
        return Content(string.Empty);
    }
}
