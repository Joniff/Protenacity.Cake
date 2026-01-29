using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

public interface IResponsiveImageBase
{
    string? AlternateTitle { get; }
    int? ImageQuality { get; }
    string? BackgroundId { get; }   // If set will create background image css with this Id
    int Opacity { get; }   // Will lighten or darken image so text is easier to see when placed apon it
    int WidthFactorImage { get; }    // 100 = normal ratio, 50 = double width to height, 200 = double height to width
    int WidthFactorContainer { get; }    // 100 = normal ratio, 50 = double width to height, 200 = double height to width
    ResponseImageRoundedEdges RoundedEdges { get; }
}
