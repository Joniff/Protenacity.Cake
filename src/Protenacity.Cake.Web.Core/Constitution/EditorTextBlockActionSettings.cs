using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTextBlockActionSettings
{
    public ActionStyles StyleActionTyped => ActionStyles.ParseByDescription(this.StyleAction) ?? ActionStyles.Button;
}
