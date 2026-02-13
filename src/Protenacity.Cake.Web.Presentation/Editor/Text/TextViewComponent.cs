using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;

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
        TempData[Name + nameof(EditorSubthemes)] = (int?) (content.Block.Settings as IEditorBackgroundSettings)?.Subtheme;
        TempData[Name + nameof(EditorThemeShades)] = (int?) (content.Block.Settings as IEditorBackgroundSettings)?.ThemeShade;

        return View(block);
    }
}
