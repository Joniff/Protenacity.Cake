using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTableSourceFile
{
    public EditorTableSourceFileSeparators SeparatorTyped => EditorTableSourceFileSeparators.ParseByDescription(this.Separator.ToString()) ?? EditorTableSourceFileSeparators.Comma;
}
