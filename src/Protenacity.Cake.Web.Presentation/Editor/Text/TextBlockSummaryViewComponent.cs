using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockSummaryViewComponent : ThemeViewComponent
{
    public const string Name = "TextBlockSummary";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockSummary;
        var settings = block.Settings as EditorTextBlockSummarySettings;

        if (content == null || settings == null)
        {
            return Content(string.Empty);
        }

        return View(new TextBlockSummaryViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Header = content?.Header?.HasContent() == true ? content.Header : null,
            Text = content?.Text?.HasContent() == true ? content.Text : new HtmlEncodedString(nameof(TextBlockExpandableTextViewModel.Text)),
            Subtheme = settings.Subtheme == Core.Property.EditorSubthemes.Inherit ? Subtheme() : settings.Subtheme,
            Shade = settings.ThemeShade == Core.Property.EditorThemeShades.Inherit ? ThemeShade() : settings.ThemeShade,
            OverrideColor = settings.OverrideColor,
            BorderColor = settings.BorderColor?.Color,
            BorderEdges = settings.BorderEdges
        });
    }
}
