using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Subfooter;

public class SubfooterViewModel
{
    public BlockListModel? OverrideColor { get; init; }
    public DateTime? LastUpdated { get; init; }
    public DateTime? NextReviewed { get; init; }
    public bool ShowReturnToTop { get; init; }
    public EditorThemeShades ReturnToTopShade { get; init; }
}
