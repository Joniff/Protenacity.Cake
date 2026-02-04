using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial class EditorTableSourceCoronerInquestApi
{
    public EditorCoronerInquestApiStages StageTyped => EditorCoronerInquestApiStages.ParseByDescription(this.Stage);
}
