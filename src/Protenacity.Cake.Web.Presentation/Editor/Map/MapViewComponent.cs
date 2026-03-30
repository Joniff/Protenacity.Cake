using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Constitution;

namespace Protenacity.Cake.Web.Presentation.Editor.Map;

public class MapViewComponent : ViewComponent
{
    public const string Name = nameof(Map);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var map = content.Block?.Content as IEditorMapBase;
        var settings = content.Block?.Settings as IEditorMapBaseSettings;

        if (map == null || settings == null || map.MapPosition == null)
        {
            return Content("");
        }

        return View(new MapViewModel
        {
            Map = map.MapPosition,
            Ratio = ((double)(settings.Ratio ?? Core.Property.EditorMapRatios.Ratio1x1)) / 36.0
        });
    }
}
