using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(Text);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content.Block == null)
        {
            return Content(string.Empty);
        }

        var block = editorService.Cast<IEditorTextEmbedded, IEditorTextEmbeddedSettings>(content.Block);
        TempData[Name + nameof(EditorSubthemes)] = (int?) (content.Block.Settings as IEditorBackgroundSettings)?.SubthemeTyped;
        TempData[Name + nameof(EditorThemeShades)] = (int?) (content.Block.Settings as IEditorBackgroundSettings)?.ThemeShadeTyped;

        return View(block);
    }
}
