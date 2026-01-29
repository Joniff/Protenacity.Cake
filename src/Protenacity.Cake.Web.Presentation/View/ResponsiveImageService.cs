using Protenacity.Cake.Web.Core.Property;
using System.Web;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.View;

internal class ResponsiveImageService : IResponsiveImageService
{
    private int[][] BreakpointFull = {
        new int[] { 2560, 5120, 5120 },
        new int[] { 1200, 2560, 2560 },
        new int[] { 768, 1200, 1200 },
        new int[] { 480, 768, 768 },
        new int[] { 0, 480, 480 }
    };

    private int[][] BreakpointBanner = {
        new int[] { 2560, 5120, 600 },
        new int[] { 1200, 2560, 600 },
        new int[] { 768, 1200, 480 },
        new int[] { 480, 768, 320 },
        new int[] { 0, 480, 240 }
    };

    private int[][] BreakpointPoster = {
        new int[] { 2560, 5120, 3200 },
        new int[] { 1200, 2560, 1600 },
        new int[] { 768, 1200, 750 },
        new int[] { 480, 768, 480 },
        new int[] { 0, 480, 300 }
    };

    private int[][] BreakpointCard = {
        new int[] { 2560, 5120, 3200 },
        new int[] { 1200, 2560, 1600 },
        new int[] { 768, 1200, 750 },
        new int[] { 480, 768, 480 },
        new int[] { 0, 400, 250 }
    };

    private int[][] BreakpointLogo = {
        new int[] { 0, 90, 90 }
    };

    public IEnumerable<Tuple<int?, string>> ImageUrls(MediaWithCrops? media, EditorImageCrops crop, int widthFactor, int quality)
    {
        int[][]? breakpoints = null;
        var imageUrls = new List<Tuple<int?, string>>();
        var cropName = string.Empty;

        switch (crop)
        {
            case EditorImageCrops.Full:
                breakpoints = BreakpointFull;
                cropName = "Full Screen Image";
                break;

            case EditorImageCrops.Banner:
                breakpoints = BreakpointBanner;
                cropName = "Hero Banner";
                break;

            case EditorImageCrops.Poster:
                breakpoints = BreakpointPoster;
                cropName = "Poster";
                break;

            case EditorImageCrops.Card:
                breakpoints = BreakpointCard;
                cropName = "Card";
                break;

            case EditorImageCrops.Logo:
                breakpoints = BreakpointLogo;
                cropName = "Logo";
                break;

            default:
                throw new ArgumentException("Unknown crop " + crop);
        }

        if (media == null)
        {
            var last = breakpoints.Last();
            var width = last[1];
            var height = (last[2] * widthFactor) / 100;
            var base64 = $"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{width}\" height=\"{height}\" viewBox=\"0 0 {width} {height}\"><path d=\"M0,0h1v1H0\" fill=\"#00f\"/></svg>";
            imageUrls.Add(new Tuple<int?, string>(null, HttpUtility.HtmlDecode("data:image/svg+xml;base64," + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(base64)))));
        }
        else
        {
            foreach (var breakpoint in breakpoints)
            {
                var url = media.GetCropUrl(
                    width: breakpoint[1], 
                    height: (breakpoint[2] * widthFactor) / 100, 
                    cropAlias: cropName, 
                    quality: quality, 
                    imageCropMode: ImageCropMode.Crop, 
                    preferFocalPoint: true, 
                    cacheBuster: false)
                    
                    ?? media.Url();
                imageUrls.Add(new Tuple<int?, string>(breakpoint[0] == 0 ? null : breakpoint[0], HttpUtility.HtmlDecode(url).ToString()));
            }
        }
        return imageUrls;
    }
}
