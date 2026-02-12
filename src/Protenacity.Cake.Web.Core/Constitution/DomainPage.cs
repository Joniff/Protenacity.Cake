using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class DomainPage
{
    public CookieConsentPositions CookieConsentModalPositionTyped => CookieConsentPositions.ParseByDescription(this.CookieConsentModalPosition.ToString()) ?? CookieConsentPositions.BottomLeft;
    public EditorBorderEdges ConfigDefaultAsideBorderEdgesTyped => EditorBorderEdges.ParseByDescription(this.ConfigDefaultAsideBorderEdges, EditorBorderEdges.All) ?? EditorBorderEdges.None;
}
