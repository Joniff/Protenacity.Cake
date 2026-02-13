using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockFieldViewComponent(IViewService viewService) : ThemeViewComponent
{
    public const string Name = "TextBlockField";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockField;
        var settings = block.Settings as EditorTextBlockFieldSettings;

        if (content == null || settings == null)
        {
            return Content(string.Empty);
        }

        string? text = null;

        switch (content.Field)
        {
            case Core.Property.EditorTextFieldTypes.Week:
                text = RichTextFields.Week;
                break;

            case Core.Property.EditorTextFieldTypes.Day:
                text = RichTextFields.Day;
                break;

            case Core.Property.EditorTextFieldTypes.Month:
                text = RichTextFields.Month;
                break;

            case Core.Property.EditorTextFieldTypes.Year:
                text = RichTextFields.Year;
                break;

            case Core.Property.EditorTextFieldTypes.Criteria:
                text = RichTextFields.Criteria;
                break;

            case Core.Property.EditorTextFieldTypes.Category:
                text = RichTextFields.Category;
                break;

            case Core.Property.EditorTextFieldTypes.CategoryHeading:
                text = RichTextFields.CategoryHeading;
                break;

            default:
                return Content(string.Empty);
        }

        IDictionary<string, string>? extra = null;
        var parse = viewService.Parse(text, extra);
        if (string.IsNullOrEmpty(parse))
        {
            return Content(string.Empty);
        }

        return View(new TextBlockFieldViewModel
        {
            Text = new HtmlEncodedString(parse),
            Subtheme = settings.Subtheme == Core.Property.EditorSubthemes.Inherit ? Subtheme() : settings.Subtheme,
            Shade = settings.ThemeShade == Core.Property.EditorThemeShades.Inherit ? ThemeShade() : settings.ThemeShade,
            OverrideColor = settings.OverrideColor
        });
    }
}
