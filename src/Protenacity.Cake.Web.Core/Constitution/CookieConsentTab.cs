using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface ICookieConsentTab
{
    CookieConsentPositions CookieConsentModalPositionTyped { get; }
}

public partial class CookieConsentTab
{
    public CookieConsentPositions CookieConsentModalPositionTyped => CookieConsentPositions.ParseByDescription(this.CookieConsentModalPosition.ToString()) ?? CookieConsentPositions.BottomLeft;
}
