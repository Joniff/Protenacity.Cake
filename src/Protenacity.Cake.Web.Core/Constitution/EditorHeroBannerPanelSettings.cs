using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorHeroBannerPanelSettings
{
    public EditorHeroBannerTextAlignments AlignmentTyped => Enum<EditorHeroBannerTextAlignments>.GetValueByDescription(this.Alignment);
}
