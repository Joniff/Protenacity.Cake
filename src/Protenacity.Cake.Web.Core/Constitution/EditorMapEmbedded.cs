using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorMapEmbedded
{
    public EditorMapTypes MapTypeTyped => EditorMapTypes.ParseByDescription(this.MapType);
}
