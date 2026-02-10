using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTextBlockIconText
{
    public EditorNamedIcons IconTyped => EditorNamedIcons.ParseByDescription(this.Icon) ?? EditorNamedIcons.Asterisk;
}
