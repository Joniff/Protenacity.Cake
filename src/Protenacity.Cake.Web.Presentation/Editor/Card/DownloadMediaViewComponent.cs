using Microsoft.AspNetCore.Mvc;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;
using static Lucene.Net.Util.Packed.PackedInt32s;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public class DownloadMediaViewComponent 
    : BaseViewComponent
{
    public const string Name = "DownloadMedia";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var mediaFile = content.Block?.Content as IPublishedContent;
        ArgumentNullException.ThrowIfNull(mediaFile);

        var id = Guid.NewGuid().ToString("N");
        var format = mediaFile.GetProperty(Constants.Conventions.Media.Extension)?.GetValue() as string ?? "";
        var size = ((long)(mediaFile.GetProperty(Constants.Conventions.Media.Bytes)?.GetValue() ?? 0L)).ToReadableFileSize();
        return View(new DownloadMediaViewModel
        {
            Id = Name + id,
            Action = new ActionViewModel
            {
                Download = true,
                Name = new HtmlEncodedString("Download"),
                Target = ActionTargets.NewTab,
                Subtheme = Subtheme(content),
                ThemeShade = ThemeShade(content),
                Url = mediaFile.Url()
            },
            Header = new HtmlEncodedString(mediaFile.Name),
            Text = new HtmlEncodedString((format.ToUpper() + " " + size.ToString()).Trim()),
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content)
        });
    }
}
