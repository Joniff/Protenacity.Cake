using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class DomainPage
{
    public StructureCookieConsentPositions CookieConsentModalPositionTyped => Enum<StructureCookieConsentPositions>.GetValueByDescription(this.CookieConsentModalPosition);
    public EditorBorderEdges ConfigDefaultAsideBorderEdgesTyped => Enum<EditorBorderEdges>.GetValueByDescription(this.ConfigDefaultAsideBorderEdges, EditorBorderEdges.All);
}
