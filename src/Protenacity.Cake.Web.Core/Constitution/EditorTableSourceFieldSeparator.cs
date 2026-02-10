using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorTableSourceFieldSeparator
{
    EditorTableSourceFileSeparators SeparatorTyped { get; }
}

public partial class EditorTableSourceFieldSeparator
{
    public EditorTableSourceFileSeparators SeparatorTyped => EditorTableSourceFileSeparators.ParseByDescription(this.Separator) ?? EditorTableSourceFileSeparators.Comma;
}
