using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorMapPrimary
{
    public EditorMapTypes MapTypeTyped => EditorMapTypes.ParseByDescription(this.MapType);
}
