using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockIconTextViewComponent : ThemeViewComponent
{
    public const string Name = "TextBlockIconText";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public const string MailTo = "mailto:";

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockIconText;
        var settings = block.Settings as EditorTextBlockIconTextSettings;

        if (content == null || settings == null)
        {
            return Content(string.Empty);
        }

        var url = content.Link?.Url;

        if (content.IconTyped == EditorNamedIcons.Email && url?.StartsWith(MailTo, true, null) == false && url?.IsValidEmail() == true)
        {
            url = MailTo + url;
        }

        return View(new TextBlockIconTextViewModel
        {
            Icon = content.IconTyped,
            Text = content.Text ?? new HtmlEncodedString(""),
            Subtheme = settings.SubthemeTyped == Core.Property.EditorSubthemes.Inherit ? Subtheme() : settings.SubthemeTyped,
            Shade = settings.ThemeShadeTyped == Core.Property.EditorThemeShades.Inherit ? ThemeShade() : settings.ThemeShadeTyped,
            OverrideColor = settings.OverrideColor,
            IconColor = settings.IconColor?.Color,
            LinkUrl = url,
            LinkTarget = string.IsNullOrWhiteSpace(content.Link?.Target)
                ? ActionTargets.CurrentTab
                : Enum<ActionTargets>.GetValueByDescription(content.Link?.Target)
        });
    }
}
