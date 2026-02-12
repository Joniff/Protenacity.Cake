using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;

namespace Protenacity.Cake.Web.Presentation.Editor.Subfooter;

public class SubfooterViewComponent(IViewService viewService) 
    : ViewComponent
{
    public const string Name = nameof(Subfooter);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(ISubfooterTab? subfooter)
    {
        if (subfooter == null 
            || (subfooter.SubfooterShowLastUpdatedDate == false 
            && subfooter.SubfooterShowNextReviewDate == false 
            && subfooter.SubfooterShowReturnToTopIcon == false))
        {
            return Content(string.Empty);
        }

        var currentPage = viewService.CurrentPage as EditorPage 
            ?? throw new ApplicationException("Needs to be a " + nameof(EditorPage));

        var showReviewDate = subfooter.SubfooterShowNextReviewDate;

        return View(new SubfooterViewModel
        {
            OverrideColor = subfooter.SubfooterColor,
            LastUpdated = subfooter.SubfooterShowLastUpdatedDate ? currentPage.UpdateDate : null,
            ShowReturnToTop = subfooter.SubfooterShowReturnToTopIcon,
            ReturnToTopShade = viewService.CurrentThemeShade == Core.Property.EditorThemeShades.Dark ? Core.Property.EditorThemeShades.Light : Core.Property.EditorThemeShades.Dark
        });
    }
}
