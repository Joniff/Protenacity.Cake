using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Form;

public class FormViewComponent : ViewComponent
{
    public const string Name = nameof(Form);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var formContent = content.Block?.Content as IEditorFormEmbedded;
        var formSettings = content.Block?.Settings as IEditorFormEmbeddedSettings;

        if (formContent?.Form == null)
        {
            return Content(string.Empty);
        }

        return View(new FormViewModel
        {
            Id = Guid.NewGuid().ToString("N"),
            FormId = formContent?.Form 
                ?? throw new ApplicationException("Invalid code"),
            FormTheme = formSettings?.Theme ?? "Simple"
        });
    }
}
