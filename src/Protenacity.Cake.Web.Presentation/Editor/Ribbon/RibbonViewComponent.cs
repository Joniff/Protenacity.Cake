using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.View;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Ribbon;

public class RibbonViewComponent(
    IViewService viewService, 
    IResponsiveImageService responsiveImageService) 
    : ThemeViewComponent
{
    public const string Name = nameof(Ribbon);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    private BadgeViewModel GetBadge(BlockListItem block, int imageQuality, int widthFactor, EditorBadgeImageShape defaultImageShape)
    {
        var content = block.Content as EditorRibbonBadge;
        var setting = block.Settings as EditorRibbonBadgeSettings;

        return new BadgeViewModel
        {
            Header = content?.Header,
            Text = content?.Text,
            Url = content?.Link?.Url,
            Target = string.IsNullOrWhiteSpace(content?.Link?.Target)
                ? ActionTargets.CurrentTab
                : ActionTargets.ParseByDescription(content?.Link?.Target) ?? ActionTargets.CurrentTab,
            AlternateTitle = content?.Image?.AlternateText(),
            BackgroundId = null,
            TransparentBackground = true,
            Opacity = 0,
            ImageQuality = imageQuality,
            Urls = content?.Image == null 
                ? Enumerable.Empty<Tuple<int?, string>>()
                : responsiveImageService.ImageUrls(content?.Image, EditorImageCrops.Poster, widthFactor, imageQuality),
            WidthFactorImage = widthFactor,
            WidthFactorContainer = widthFactor,
            Subtheme = Subtheme(setting),
            ThemeShade = ThemeShade(setting),
            OverrideColor = setting?.OverrideColor,
            BorderColor = setting?.BorderColor?.Color,
            BorderEdges = setting?.BorderEdges ?? EditorBorderEdges.None,
            Shape = setting?.ImageShape == EditorBadgeImageShape.Default ? defaultImageShape : setting?.ImageShape ?? EditorBadgeImageShape.Square
        };
    }

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var ribbon = content.Block?.Content as IEditorRibbonBase;
        var ribbonSettings = content.Block?.Settings as IEditorRibbonBaseSettings;
        var imageQuality = viewService.CurrentDomainPage.ConfigImageQuality;
        var imageSize = ribbonSettings?.ImageSize ?? EditorCardStyleImageSizes.Medium;
        var widthFactor = WidthFactor(imageSize);

        if (ribbon?.Badges?.Any() != true)
        {
            // We have no badges to show
            return Content(string.Empty);
        }

        return View(new RibbonViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Badges = ribbon.Badges.Select(b => GetBadge(b, imageQuality, widthFactor, ribbonSettings?.DefaultImageShape ?? EditorBadgeImageShape.Square)),
            ImageSize = imageSize,
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content),
        });
    }
}
