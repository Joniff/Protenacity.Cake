using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;
public partial class EditorSelectMediaAsideSettings
{
    public EditorOrders OrderTyped => Enum<EditorOrders>.GetValueByDescription(this.Order);
}
