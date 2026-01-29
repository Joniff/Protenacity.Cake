using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.HeroBanner;

public class HeroBannerViewComponent : ViewComponent
{
    public const string Name = nameof(HeroBanner);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(BlockListModel blocks)
    {
        return View(blocks.Select((b, i) => new HeroBannerViewModel
        {
            Index = i,
            Block = (BlockListItem<EditorHeroBannerPanel, EditorHeroBannerPanelSettings>)b
        }));
    }
}
