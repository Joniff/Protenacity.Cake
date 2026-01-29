using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Alert;

public class AlertViewComponent : ViewComponent
{
    public const string Name = nameof(Alert);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IAlertTab alert)
    {
        return alert != null ? View(alert) : Content(string.Empty);
    }
}
