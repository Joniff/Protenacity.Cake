using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorVideoBaseSettings
{
    EditorVideoRatios RatioTyped { get; }
}

public partial class EditorVideoBaseSettings
{
    public EditorVideoRatios RatioTyped => EditorVideoRatios.ParseByDescription(this.Ratio) ?? EditorVideoRatios.SixteenByNine;
}
