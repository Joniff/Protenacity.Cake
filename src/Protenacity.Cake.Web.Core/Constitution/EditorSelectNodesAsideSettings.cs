using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;
public partial class EditorSelectNodesAsideSettings
{
    public EditorOrders OrderTyped => EditorOrders.ParseByDescription(this.Order) ?? EditorOrders.Default;
}
