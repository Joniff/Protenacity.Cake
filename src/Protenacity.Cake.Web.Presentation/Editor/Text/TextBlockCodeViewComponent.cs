using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;
using Protenacity.Cake.Web.Core.Extensions;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockCodeViewComponent : ThemeViewComponent
{
    public const string Name = "TextBlockCode";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    private bool FirstTime(EditorCodeLanguage? language = null)
    {
        var contextKey = "e6baceea-5873-45c8-a0be-9bea1d2437a0" + (language?.Description ?? "");
        var value = HttpContext.Items[contextKey] == null;
        HttpContext.Items[contextKey] = true;
        return value;
    }

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockCode;
        var settings = block.Settings as EditorTextBlockCodeSettings;

        if (string.IsNullOrWhiteSpace(content?.Code) || settings == null)
        {
            return Content(string.Empty);
        }

        return View(new TextBlockCodeViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Language = content.Language ?? EditorCodeLanguage.CSharp,
            Text = content.Code,
            Subtheme = settings.Subtheme ?? EditorSubthemes.Primary,
            Shade = settings.ThemeShade ?? EditorThemeShades.Light,
            OverrideColor = settings.OverrideColor,
            EnableSyntaxHighlighting = settings.SyntaxHighlight,
            AddDependencyLibrary = FirstTime(),
            AddDependencyLibraryLanguage = FirstTime(content.Language),
        });
    }
}
