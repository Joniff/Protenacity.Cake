using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorActionDefaultSettings
{
    ActionStyles StyleActionTyped { get; }
    ActionStyleClickAreas StyleActionClickAreaTyped { get; }
}

public partial class EditorActionDefaultSettings
{
    public ActionStyles StyleActionTyped => ActionStyles.Button; // QWERTY
    public ActionStyleClickAreas StyleActionClickAreaTyped => ActionStyleClickAreas.Action;
}
