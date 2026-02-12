using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class MediaViewComponent(IViewService viewService, 
    IResponsiveImageService responsiveImageService) 
    : BaseViewComponent
{
    public const string Name = "Media";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        var media = content.Block?.Content as MediaWithCrops;
        var styleImageLocation = StyleImageLocation(content);
        var styleImageSize = StyleImageSize(content);
        var styleHeader = StyleHeader(content);
        var styleDate = StyleDate(content);
        var styleTime = StyleTime(content);
        var styleText = StyleText(content);

        IActionViewModel? action = content?.Block?.Content == null ? null : new ActionViewModel
        {
            Style = StyleAction(content),
            Alignment = StyleActionAlignment(content),
            ShowClickArrow = StyleShowClickArrow(content),
            Name = new HtmlEncodedString(media?.Name ?? media?.Url() ?? Name),
            Target = ActionTargets.CurrentTab,
            Url = media?.Url(),
            Download = (content.Block?.Settings as IEditorSelectMediaDownloadSettings)?.Download ?? false,
            TextRestrict = content.Defaults.TextRestrict
        };

        if (styleImageLocation != EditorCardStyleImageLocations.Hide || styleHeader != EditorCardStyleHeaders.Hide || styleText != EditorCardStyleTexts.Hide)
        {
            var imageQuality = viewService.CurrentDomainPage.ConfigImageQuality;
            var id = Guid.NewGuid().ToString("N");
            var size = ((long)(content!.Block?.Content?.GetProperty(Constants.Conventions.Media.Bytes)?.GetValue() ?? 0L)).ToReadableFileSize();
            var widthFactor = WidthFactor(styleImageSize);
            return View(GetTemplate(media != null, styleImageLocation), new CardViewModel
            {
                Id = Name + id,
                AlternateTitle = media?.AlternateText(),
                BackgroundId = null,
                Opacity = 0,
                ImageQuality = imageQuality,
                Urls = responsiveImageService.ImageUrls(media, EditorImageCrops.Poster, widthFactor, imageQuality),
                WidthFactorImage = widthFactor,
                WidthFactorContainer = styleImageLocation == EditorCardStyleImageLocations.Top ? widthFactor : 100,
                RoundedEdges = GetRoundedEdges(media != null, styleImageLocation),
                Action = action == null ? null : new ActionViewModel
                {
                    Download = action.Download,
                    Name = new HtmlEncodedString(action.Download ? "Download" : "View"),
                    Style = action.Style,
                    Target = ActionTargets.NewTab,
                    Subtheme = Subtheme(content),
                    ThemeShade = ThemeShade(content),
                    Url = action.Url,
                },
                Header = action?.Name,
                Date = media?.UpdateDate,
                DateFormat = viewService.CurrentDomainPage.ConfigCardDateFormat,
                TimeFormat = viewService.CurrentDomainPage.ConfigCardTimeFormat,
                Text = new HtmlEncodedString("Size = " + size.ToString()),
                HeaderStyle = styleHeader,
                DateStyle = styleDate,
                TimeStyle = styleTime,
                ImageStyleLocation = styleImageLocation,
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
