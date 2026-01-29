using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public abstract class ResponsiveImageBase : IResponsiveImageBase
{
    public string? AlternateTitle { get; init; }
    public int? ImageQuality { get; set; }
    public string? BackgroundId { get; set; }   // If set will create background image css with this Id
    public int Opacity { get; set; }   // Will lighten or darken image so text is easier to see when placed apon it
    public int WidthFactorImage { get; set; }   // 100 = normal ratio, 50 = double width to height, 200 = double height to width
    public int WidthFactorContainer { get; set; }   // 100 = normal ratio, 50 = double width to height, 200 = double height to width
    public ResponseImageRoundedEdges RoundedEdges { get; set; }
}
