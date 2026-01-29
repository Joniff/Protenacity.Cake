using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.NamedIcon;

public class NamedIconViewComponent : ThemeViewComponent
{
    public const string Name = nameof(NamedIcon);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(EditorNamedIcons icon)
    {
        return View(icon);
    }
}
