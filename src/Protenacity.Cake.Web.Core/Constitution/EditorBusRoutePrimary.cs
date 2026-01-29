using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorBusRoutePrimary
{
    public EditorBusRouteTypes DisplayTypeTyped => Enum<EditorBusRouteTypes>.GetValueByDescription(this.DisplayType);
}
