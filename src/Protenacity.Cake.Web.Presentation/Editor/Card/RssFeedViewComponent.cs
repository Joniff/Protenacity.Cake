using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Sagara.FeedReader;
using System.Xml.Linq;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class RssFeedViewComponent(IViewService viewService) : BaseViewComponent
{
    public const string Name = "RssFeed";

    public IViewComponentResult Invoke(IEditorContent<EditorFeedItem> content)
    {
        var rssfeedContent = content.Block?.Content as EditorRssFeed;

        if (content.ExtraData == null || rssfeedContent == null)
        {
            return Content(string.Empty);
        }
        var styleImageLocation = StyleImageLocation(content);
        var styleImageSize = StyleImageSize(content);
        var styleHeader = StyleHeader(content);
        var styleDate = StyleDate(content);
        var styleTime = StyleTime(content);
        var styleText = StyleText(content);
        var name = (string.IsNullOrWhiteSpace(content.ExtraData?.Title) ? content.ExtraData?.Title : content.ExtraData?.Title) ?? Name;

        var action = new ActionViewModel
        {
            Style = StyleAction(content),
            Alignment = StyleActionAlignment(content),
            ShowClickArrow = StyleShowClickArrow(content),
            Name = new HtmlEncodedString(name),
            Target = ActionTargets.CurrentTab,
            Url = content.ExtraData?.Link,
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            Download = false,
            TextRestrict = content.Defaults.TextRestrict
        };

        if (styleImageLocation != EditorCardStyleImageLocations.Hide || styleHeader != EditorCardStyleHeaders.Hide || styleText != EditorCardStyleTexts.Hide)
        {
            var imageQuality = viewService.CurrentDomainPage.ConfigImageQuality;
            var id = Guid.NewGuid().ToString("N");
            var hasImage = !string.IsNullOrWhiteSpace(content.ExtraData?.ImageUrl);
            var widthFactor = WidthFactor(styleImageSize);

            return View(GetTemplate(hasImage, styleImageLocation), new CardViewModel
            {
                Id = Name + id,
                AlternateTitle = name,
                BackgroundId = null,
                Opacity = 0,
                ImageQuality = imageQuality,
                Urls = hasImage ? new Tuple<int?, string>[] { 
                    new Tuple<int?, string>(null, content.ExtraData?.ImageUrl ?? throw new ArgumentNullException("Should have already checked for Null"))
                } : Enumerable.Empty<Tuple<int?, string>>(),
                WidthFactorImage = widthFactor,
                WidthFactorContainer = styleImageLocation == EditorCardStyleImageLocations.Top ? widthFactor : 100,
                RoundedEdges = GetRoundedEdges(hasImage, styleImageLocation),
                Action = new ActionViewModel
                {
                    Style = action.Style,
                    Name = rssfeedContent.ActionText?.HasContent() == true ? rssfeedContent.ActionText : new HtmlEncodedString(name),
                    Target = action.Target,
                    Url = action.Url,
                    Subtheme = action.Subtheme,
                    ThemeShade = action.ThemeShade,
                    Download = action.Download,
                    TextRestrict = action.TextRestrict
                },
                Header = new HtmlEncodedString(name),
                Date = content.ExtraData?.PublishingDate,
                DateFormat = viewService.CurrentDomainPage.ConfigCardDateFormat,
                TimeFormat = viewService.CurrentDomainPage.ConfigCardTimeFormat,
                Text = new HtmlEncodedString(content.ExtraData?.Description ?? ""),
                HeaderStyle = styleHeader,
                DateStyle = styleDate,
                TimeStyle = styleTime,
                ImageStyleLocation = styleImageLocation,
                ImageStyleSize = styleImageSize,
                TextStyle = styleText,
                ActionClickArea = StyleActionClickArea(content!),
                ActionAlignment = StyleActionAlignment(content!),
                Subtheme = Subtheme(content!),
                ThemeShade = ThemeShade(content!),
                OverrideColor = OverrideColor(content!),
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
