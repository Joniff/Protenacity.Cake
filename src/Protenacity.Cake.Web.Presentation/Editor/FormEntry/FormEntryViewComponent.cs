using Protenacity.Cake.Web.Presentation.Form;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.FormEntry;

public class FormEntryViewComponent : ThemeViewComponent
{
    public const string Name = nameof(FormEntry);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var entry = content as EditorContent<FormReaderEntry>;

        if (entry?.ExtraData == null)
        {
            return Content(string.Empty);
        }

        return View(new FormEntryViewModel
        {
            Id = Guid.NewGuid().ToString("N"),
            Name = entry.ExtraData.Name,
            Created = entry.ExtraData.Created,
            Message = entry.ExtraData.Message,
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content)
        });
    }
}
