using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCoronerInquestApiStages
{
    [Description("Inquest Hearings")]
    Hearing,

    [Description("Inquest Openings")]
    Opening,

    [Description("Inquest Conclusions")]
    Conclusion,
}
