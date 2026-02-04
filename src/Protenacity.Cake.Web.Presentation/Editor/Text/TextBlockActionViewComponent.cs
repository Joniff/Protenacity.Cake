using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockActionViewComponent : ThemeViewComponent
{
    public const string Name = "TextBlockAction";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockAction;
        var settings = block.Settings as EditorTextBlockActionSettings;
        var actionContent = content?.Link;

        if (content == null || string.IsNullOrWhiteSpace(actionContent?.Url))
        {
            return Content(string.Empty);
        }

        return View(new TextBlockActionViewModel
        {
            Name = new HtmlEncodedString(actionContent?.Name ?? actionContent?.Url ?? ""),
            Target = string.IsNullOrWhiteSpace(actionContent?.Target)
            ? ActionTargets.CurrentTab
            : ActionTargets.ParseByDescription(actionContent?.Target),
            Url = actionContent?.Url,
            Alignment = ActionStyleAlignments.LeftRelative,
            Style = settings?.StyleActionTyped ?? ActionStyles.Button
        });
    }
}
