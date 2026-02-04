using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface ICookieConsentTab
{
    StructureCookieConsentPositions CookieConsentModalPositionTyped { get; }
}

public partial class CookieConsentTab
{
    public StructureCookieConsentPositions CookieConsentModalPositionTyped => StructureCookieConsentPositions.ParseByDescription(this.CookieConsentModalPosition);
}
