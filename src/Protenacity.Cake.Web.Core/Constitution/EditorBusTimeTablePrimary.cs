using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorBusTimeTablePrimary
{
    public EditorBusTimeTableTypes DisplayTypeTyped => Enum<EditorBusTimeTableTypes>.GetValueByDescription(this.DisplayType);
}
