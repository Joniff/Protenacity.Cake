using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class CardViewComponent(IViewService viewService, 
    IResponsiveImageService responsiveImageService)
    : BaseViewComponent
{
    public const string Name = nameof(Card);

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var cardContent = content.Block?.Content as IEditorCardBase;
        var actionContent = content.Block?.Content as IEditorActionEmbedded;
        var downloadSettings = content.Block?.Settings as IEditorSelectMediaDownloadSettings;

        var styleImageLocation = StyleImageLocation(content);
        var styleImageSize = StyleImageSize(content);
        var styleHeader = StyleHeader(content);
        var styleDate = StyleDate(content);
        var styleTime = StyleTime(content);
        var styleText = StyleText(content);

        IActionViewModel? action = string.IsNullOrWhiteSpace(actionContent?.Link?.Url) ? null : new ActionViewModel
        {
            Style = StyleAction(content),
            Alignment = StyleActionAlignment(content),
            ShowClickArrow = StyleShowClickArrow(content),
            Name = new HtmlEncodedString(actionContent?.Link?.Name ?? actionContent?.Link?.Url ?? ""),
            Target = string.IsNullOrWhiteSpace(actionContent?.Link?.Target)
                ? ActionTargets.CurrentTab
                : ActionTargets.ParseByDescription(actionContent?.Link?.Target) ?? ActionTargets.CurrentTab,
            Url = actionContent?.Link?.Url,
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            Download = downloadSettings?.Download ?? false,
            TextRestrict = content.Defaults.TextRestrict
        };

        if (styleImageLocation != EditorCardStyleImageLocations.Hide || styleHeader != EditorCardStyleHeaders.Hide || styleText != EditorCardStyleTexts.Hide)
        {
            var imageQuality = viewService.CurrentDomainPage.ConfigImageQuality;
            var id = Guid.NewGuid().ToString("N");
            var widthFactor = WidthFactor(styleImageSize);
            return View(GetTemplate(cardContent?.Image != null, styleImageLocation), new CardViewModel
            {
                Id = Name + id,
                AlternateTitle = cardContent?.Image?.AlternateText(),
                BackgroundId = null,
                Opacity = 0,
                ImageQuality = imageQuality,
                Urls = responsiveImageService.ImageUrls(cardContent?.Image, EditorImageCrops.Poster, widthFactor, imageQuality),
                WidthFactorImage = widthFactor,
                WidthFactorContainer = styleImageLocation == EditorCardStyleImageLocations.Top ? widthFactor : 100,
                RoundedEdges = GetRoundedEdges(cardContent?.Image != null, styleImageLocation),
                Action = action,
                Header = cardContent?.Header,
                Date = cardContent?.Date == DateTime.MinValue ? null : cardContent?.Date,
                DateFormat = viewService.CurrentDomainPage.ConfigCardDateFormat,
                TimeFormat = viewService.CurrentDomainPage.ConfigCardTimeFormat,
                Text = cardContent?.Text,
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
