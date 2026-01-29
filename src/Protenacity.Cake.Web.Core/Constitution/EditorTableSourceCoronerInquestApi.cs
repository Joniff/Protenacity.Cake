using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTableSourceCoronerInquestApi
{
    public EditorCoronerInquestApiStages StageTyped => Enum<EditorCoronerInquestApiStages>.GetValueByDescription(this.Stage);
}
