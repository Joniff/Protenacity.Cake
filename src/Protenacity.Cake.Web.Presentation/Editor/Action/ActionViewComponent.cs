using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Action;

public class ActionViewComponent : ViewComponent
{
    public const string Name = nameof(Action);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(object? model)
    {
        var action = model as IActionViewModel;
        if (action == null)
        {
            var content = model as IEditorContent
                ?? throw new ApplicationException("Can only accept types of " + nameof(IActionViewModel) + " or " + nameof(IEditorContent));

            var block = content.Block?.Content as IEditorActionEmbedded;
            var settings = content.Block?.Settings as IEditorActionEmbeddedSettings;
            var backgroundSettings = content.Block?.Settings as IEditorBackgroundSettings;

            action = new ActionViewModel
            {
                Style = settings?.StyleActionTyped ?? ActionStyles.Link,
                Alignment = settings?.StyleActionAlignmentTyped ?? ActionStyleAlignments.RightAbsolute,
                Name = new HtmlEncodedString(block?.Link?.Name ?? block?.Link?.Url ?? ""),
                Target = string.IsNullOrWhiteSpace(block?.Link?.Target) 
                    ? ActionTargets.CurrentTab 
                    : Enum<ActionTargets>.GetValueByDescription(block?.Link?.Target),
                Url = block?.Link?.Url,
                Subtheme = backgroundSettings?.SubthemeTyped ?? EditorSubthemes.Inherit,
                ThemeShade = backgroundSettings?.ThemeShadeTyped ?? EditorThemeShades.Inherit,
                Download = false
            };
        }

        return View(action);
    }
}
