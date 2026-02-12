using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTextBlockField
{
    public EditorTextFieldTypes FieldTyped => EditorTextFieldTypes.ParseByDescription(this.Field.ToString()) ?? EditorTextFieldTypes.Week;
}
