using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorBorderSettings
{
    EditorBorderEdges BorderEdgesTyped { get; }
}

public partial class EditorBorderSettings
{
    public EditorBorderEdges BorderEdgesTyped => Enum<EditorBorderEdges>.GetValueByDescription(this.BorderEdges, EditorBorderEdges.All);
}
