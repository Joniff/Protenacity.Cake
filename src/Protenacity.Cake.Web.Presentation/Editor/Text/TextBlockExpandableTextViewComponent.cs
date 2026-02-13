using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockExpandableTextViewComponent : ThemeViewComponent
{
    public const string Name = "TextBlockExpandableText";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockExpandableText;
        var settings = block.Settings as EditorTextBlockExpandableTextSettings;

        if (content == null || settings == null)
        {
            return Content(string.Empty);
        }


        return View(new TextBlockExpandableTextViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Header = content.Header ?? new HtmlEncodedString(nameof(TextBlockExpandableTextViewModel.Header)),
            Text = content.Text ?? new HtmlEncodedString(nameof(TextBlockExpandableTextViewModel.Text)),
            Subtheme = settings.Subtheme == Core.Property.EditorSubthemes.Inherit ? Subtheme(): settings.Subtheme,
            Shade = settings.ThemeShade == Core.Property.EditorThemeShades.Inherit ? ThemeShade() : settings.ThemeShade,
            OverrideColor = settings.OverrideColor,
            //IconColor = settings.IconColor?.Color,
            InitialState = settings.InitialState
        });
    }
}
