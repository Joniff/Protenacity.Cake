using Umbraco.Cms.Core.Models;

namespace Protenacity.Cake.Web.Presentation.View;

public interface IVideoService
{
    string? YouTube(string url);
    string? Vimeo(string url);
    string? Media(MediaWithCrops mediaWithCrops);
    string? Mp4(string url);
}
