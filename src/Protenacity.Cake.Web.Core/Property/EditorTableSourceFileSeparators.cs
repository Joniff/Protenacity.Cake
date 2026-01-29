using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorTableSourceFileSeparators
{
    [Description("Comma ( , )")]
    Comma,

    [Description("Semicolon ( ; )")]
    Semicolon,

    [Description("Tab ( \\t )")]
    Tab,

    [Description("Pipe ( | )")]
    Pipe
}
