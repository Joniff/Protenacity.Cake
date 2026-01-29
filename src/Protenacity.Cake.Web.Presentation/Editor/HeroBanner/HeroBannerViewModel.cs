using Protenacity.Cake.Web.Core.Constitution;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.HeroBanner;

public class HeroBannerViewModel
{
    public int Index { get; init; }
    public required BlockListItem<EditorHeroBannerPanel, EditorHeroBannerPanelSettings> Block { get; init; }
}
