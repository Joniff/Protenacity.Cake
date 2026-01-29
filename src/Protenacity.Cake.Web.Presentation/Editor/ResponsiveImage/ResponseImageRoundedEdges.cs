namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

[Flags]
public enum ResponseImageRoundedEdges
{
    None = 0,
    Left = 1,
    Right = 2,
    Top = 4,
    Bottom = 8,
    All = 15,
}
