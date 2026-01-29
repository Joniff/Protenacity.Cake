using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum ActionStyles
{
    [Description("Button")]
    Button = 0,

    [Description("Link")]
    Link = 6,

    [Description("Link with Icon")]
    IconLink = 10,

    [Description("Header")]
    Header = 18,

    [Description("Hide")]
    Hide = 16384,

    [Description("Button (Primary)")]
    ButtonPrimary = 65,

    [Description("Button (Secondary)")]
    ButtonSecondary = 129,

    [Description("Button (Success)")]
    ButtonSuccess = 257,

    [Description("Button (Danger)")]
    ButtonDanger = 513,

    [Description("Button (Warning)")]
    ButtonWarning = 1025,

    [Description("Button (Info)")]
    ButtonInfo = 2049,

    [Description("Button (Light)")]
    ButtonLight = 4097,

    [Description("Button (Dark)")]
    ButtonDark = 8193,
}
