using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorOrderSettings
{
    EditorOrders OrderTyped { get; }
}

public partial class EditorOrderSettings
{
    public EditorOrders OrderTyped => Enum<EditorOrders>.GetValueByDescription(this.Order);
}
