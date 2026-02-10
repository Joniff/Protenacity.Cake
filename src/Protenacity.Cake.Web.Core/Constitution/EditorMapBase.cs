using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorMapBase
{
    EditorMapTypes MapTypeTyped { get; }
}

public partial class EditorMapBase
{
    public EditorMapTypes MapTypeTyped => EditorMapTypes.ParseByDescription(this.MapType) ?? EditorMapTypes.Standard;
}
