using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTextBlockActionSettings
{
    public ActionStyles StyleActionTyped => Enum<ActionStyles>.GetValueByDescription(this.StyleAction);
}
