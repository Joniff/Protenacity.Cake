using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class PageViewComponent(IViewService viewService, 
    IResponsiveImageService responsiveImageService) 
    : BaseViewComponent
{
    public const string Name = "Page";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content == null)
        {
            return Content("");
        }

        var page = content.Block?.Content as EditorPage;
        var styleImageLocation = StyleImageLocation(content);
        var styleImageSize = StyleImageSize(content);
        var styleHeader = StyleHeader(content);
        var styleDate = StyleDate(content);
        var styleTime = StyleTime(content);
        var styleText = StyleText(content);
        var name = (string.IsNullOrWhiteSpace(page?.Title) ? page?.Name : page?.Title) ?? Name;

        IActionViewModel? action = content.Block?.Content == null ? null : new ActionViewModel
        {
            Style = StyleAction(content),
            Alignment = StyleActionAlignment(content),
            ShowClickArrow = StyleShowClickArrow(content),
            Name = page?.PageTitleName?.HasContent() == true ? page?.PageTitleName : new HtmlEncodedString(name),
            Target = ActionTargets.CurrentTab,
            Url = page?.Url(),
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            Download = (content.Block?.Settings as IEditorSelectMediaDownloadSettings)?.Download ?? false,
            TextRestrict = content.Defaults.TextRestrict
        };

        if (styleImageLocation != EditorCardStyleImageLocations.Hide || styleHeader != EditorCardStyleHeaders.Hide || styleText != EditorCardStyleTexts.Hide)
        {
            var imageQuality = viewService.CurrentDomainPage.ConfigImageQuality;
            var id = Guid.NewGuid().ToString("N");
            var date = (page?.SeoDatePublished ?? DateTime.MinValue) != DateTime.MinValue ? page?.SeoDatePublished : page?.UpdateDate;
            var widthFactor = WidthFactor(styleImageSize);

            return View(GetTemplate(page?.SeoThumbnail != null, styleImageLocation), new CardViewModel
            {
                Id = Name + id,
                AlternateTitle = page?.SeoThumbnail?.AlternateText(),
                BackgroundId = null,
                Opacity = 0,
                ImageQuality = imageQuality,
                Urls = responsiveImageService.ImageUrls(page?.SeoThumbnail, EditorImageCrops.Poster, widthFactor, imageQuality),
                WidthFactorImage = widthFactor,
                WidthFactorContainer = styleImageLocation == EditorCardStyleImageLocations.Top ? widthFactor : 100,
                RoundedEdges = GetRoundedEdges(page?.SeoThumbnail != null, styleImageLocation),
                Action = action,
                Header = action?.Name,
                Date = date,
                DateFormat = viewService.CurrentDomainPage.ConfigCardDateFormat,
                TimeFormat = viewService.CurrentDomainPage.ConfigCardTimeFormat,
                Text = page?.SeoAbstract?.HasContent() == true ? page?.SeoAbstract : new HtmlEncodedString(page?.SeoDescription ?? ""),
                HeaderStyle = styleHeader,
                DateStyle = styleDate,
                TimeStyle = styleTime,
                ImageStyleLocation = styleImageLocation,
                ImageStyleSize = styleImageSize,
                TextStyle = styleText,
                ActionClickArea = StyleActionClickArea(content),
                ActionAlignment = StyleActionAlignment(content),
                Subtheme = Subtheme(content),
                ThemeShade = ThemeShade(content),
                OverrideColor = OverrideColor(content),
                BorderColor = BorderColor(content),
                BorderEdges = BorderEdges(content)
            });
        }
        else if (action != null)
        {
            return View(ActionViewComponent.Template, action);
        }
        return Content(string.Empty);
    }
}
