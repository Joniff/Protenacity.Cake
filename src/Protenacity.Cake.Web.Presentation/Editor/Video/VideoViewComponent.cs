using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Video;

public class VideoViewComponent(IViewService viewService, 
    IEditorService editorService, 
    IResponsiveImageService responsiveImageService, 
    IVideoService videoService) 
    : ViewComponent
{
    public const string Name = nameof(Video);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content.Block == null)
        {
            return Content(string.Empty);
        }

        var video = editorService.Cast<IEditorVideoEmbedded, IEditorVideoBaseSettings>(content.Block);
        var source = video.Content.Source?.FirstOrDefault()?.Content;
        var sourceType = VideoViewModel.SourceTypes.Media;
        string? sourceCode = null;

        if (source == null)
        {
            return Content(string.Empty);
        }

        switch (source.ContentType.Alias)
        {
            case EditorVideoSourceMedia.ModelTypeAlias:
                {
                    sourceType = VideoViewModel.SourceTypes.Media;
                    var media = ((EditorVideoSourceMedia)source)?.Video;
                    if (media != null)
                    {
                        sourceCode = videoService.Media(media);
                    }
                }
                break;

            case EditorVideoSourceYoutube.ModelTypeAlias:
                {
                    sourceType = VideoViewModel.SourceTypes.YouTube;
                    var url = ((EditorVideoSourceYoutube)source)?.Url;
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        sourceCode = videoService.YouTube(url);
                    }
                }
                break;

            case EditorVideoSourceVimeo.ModelTypeAlias:
                {
                    sourceType = VideoViewModel.SourceTypes.Vimeo;
                    var url = ((EditorVideoSourceVimeo)source)?.Url;
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        sourceCode = videoService.Vimeo(url);
                    }
                }
                break;

        }

        if (string.IsNullOrWhiteSpace(sourceCode))
        {
            return Content(string.Empty);
        }

        var configImageQuality = viewService.CurrentDomainPage.ConfigImageQuality;

        return View(new VideoViewModel
        {
            Id = nameof(Video) + Guid.NewGuid().ToString("N"),
            Urls = responsiveImageService.ImageUrls(video.Content.Poster, Core.Property.EditorImageCrops.Poster, 100, configImageQuality),
            Opacity = 0,
            ImageQuality = configImageQuality,
            Header = video.Content.Header,
            Copyright = video.Content.Copyright,
            SourceType = sourceType,
            SourceCode = sourceCode,
            Ratio = video.Settings?.Ratio ?? Core.Property.EditorVideoRatios.SixteenByNine,
            ShowControls = video.Settings?.ShowControls ?? false,
        });
    }
}
