using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class DomainPage
{
    public StructureCookieConsentPositions CookieConsentModalPositionTyped => StructureCookieConsentPositions.ParseByDescription(this.CookieConsentModalPosition) ?? StructureCookieConsentPositions.BottomLeft;
    public EditorBorderEdges ConfigDefaultAsideBorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.ConfigDefaultAsideBorderEdges, EditorBorderEdges.All) ?? EditorBorderEdges.None;
}
