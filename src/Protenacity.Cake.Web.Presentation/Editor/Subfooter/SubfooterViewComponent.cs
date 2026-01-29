using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

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
        var reviewDate = currentPage.ReviewDate;
        if (showReviewDate)
        {
            IPublishedContent? node = currentPage;
            while (node != null && node.Id != Constants.System.Root)
            {
                if (node.ContentType.Alias == EditorPage.ModelTypeAlias)
                {
                    switch ((node as EditorPage)?.ReviewStatusTyped)
                    {
                        case Core.Property.ReviewStatuses.Inherit:
                            if (reviewDate == DateTime.MinValue)
                            {
                                reviewDate = (node as EditorPage)?.ReviewDate ?? DateTime.MinValue;
                            }
                            node = node.Parent();
                            continue;

                        case Core.Property.ReviewStatuses.Disable:
                            showReviewDate = false;
                            break;
                    }
                    break;
                }
            }
        }

        return View(new SubfooterViewModel
        {
            OverrideColor = subfooter.SubfooterColor,
            LastUpdated = subfooter.SubfooterShowLastUpdatedDate ? currentPage.UpdateDate : null,
            NextReviewed = showReviewDate && reviewDate != DateTime.MinValue ? reviewDate : null,
            ShowReturnToTop = subfooter.SubfooterShowReturnToTopIcon,
            ReturnToTopShade = viewService.CurrentThemeShade == Core.Property.EditorThemeShades.Dark ? Core.Property.EditorThemeShades.Light : Core.Property.EditorThemeShades.Dark
        });
    }
}
