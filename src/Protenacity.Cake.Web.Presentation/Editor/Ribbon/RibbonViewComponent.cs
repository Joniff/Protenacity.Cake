using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.View;

namespace Protenacity.Cake.Web.Presentation.Editor.Ribbon;

public class RibbonViewComponent(
    IViewService viewService, 
    IResponsiveImageService responsiveImageService) 
    : ThemeViewComponent
{
    public const string Name = nameof(Ribbon);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var ribbon = content.Block?.Content as IEditorRibbonBase;
        var ribbonSettings = content.Block?.Settings as IEditorRibbonBaseSettings;
        var imageQuality = viewService.CurrentDomainPage.ConfigImageQuality;
        var widthFactor = 100;

        if (ribbon?.Badges?.Any() != true)
        {
            // We have no badges to show
            return Content(string.Empty);
        }

        return View(new RibbonViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Badges = ribbon.Badges.Select(b => new BadgeViewModel
            {
                Header = (b.Content as EditorRibbonBadge)?.Header,
                Text = (b.Content as EditorRibbonBadge)?.Text,
                AlternateTitle = (b.Content as EditorRibbonBadge)?.Icon?.AlternateText(),
                BackgroundId = null,
                Opacity = 0,
                ImageQuality = imageQuality,
                Urls = responsiveImageService.ImageUrls((b.Content as EditorRibbonBadge)?.Icon, EditorImageCrops.Poster, widthFactor, imageQuality),
                WidthFactorImage = widthFactor,
                WidthFactorContainer = widthFactor,
                Subtheme = Subtheme(b.Content),
                ThemeShade = ThemeShade(b.Content),
                OverrideColor = (b?.Settings as IEditorBackgroundSettings)?.OverrideColor,
                BorderColor = (b.Settings as IEditorBorderSettings)?.BorderColor?.Color,
                BorderEdges = (b.Settings as IEditorBorderSettings)?.BorderEdges ?? EditorBorderEdges.None,
                Shape = (b.Settings as EditorRibbonBadgeSettings)?.IconShape ?? ribbonSettings?.DefaultIconShape ?? EditorBadgeIconShape.Square
            }),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content),
        });
    }
}
