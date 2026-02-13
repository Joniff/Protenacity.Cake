using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Pages.Domain;

public class RssFeedMiddleware(IRuntimeState runtimeState, 
    IFusionCache fusionCache, 
    IViewService viewService, 
    ILogger<RssFeedMiddleware> logger) : IMiddleware
{
    private XNamespace rssFeedNamespace = "https://www.rssboard.org/rss-specification";

    //private static Lazy<RecyclableMemoryStreamManager> MemoryManager = new Lazy<RecyclableMemoryStreamManager>(() =>
    //{
    //    return new RecyclableMemoryStreamManager();
    //});

    private RssFeedEntryTab? GetRssFeedEntry(DomainPage domain, string path)
    {
        if (domain?.RssFeeds?.Any() != true)
        {
            return null;
        }

        foreach (var entry in domain.RssFeeds.Select(x => x.Content as RssFeedEntryTab))
        {
            if (string.IsNullOrEmpty(entry?.Url))
            {
                continue;
            }
            var trimUrl = entry.Url.Trim();

            if (string.Compare(path, trimUrl, true) == 0 || string.Compare(path, "/" + trimUrl, true) == 0)
            {
                return entry;
            }
        }
        return null;
    }

    private IEnumerable<XElement> Pages(IPublishedContent? parent, bool includeDecendants = false)
    {
        var currentUri = viewService.CurrentUri;
        var pages = parent?.Children<EditorPage>()?.Where(p => p.SeoStatus != SeoStatuses.Disable);
        if (pages?.Any() == true)
        {
            foreach (var page in pages)
            {
                yield return new XElement(rssFeedNamespace + "item",
                    new XElement(rssFeedNamespace + "link", new Uri(currentUri, page.Url())),
                    new XElement(rssFeedNamespace + "pubDate", (page.SeoDatePublished != DateTime.MinValue ? page.SeoDatePublished : page.UpdateDate).ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss zzz")),
                    new XElement(rssFeedNamespace + "title", string.IsNullOrWhiteSpace(page.Title) ? page.Name : page.Title),
                    new XElement(rssFeedNamespace + "description", string.IsNullOrWhiteSpace(page.SeoDescription) ? page.SeoAbstract?.ToText() : page.SeoDescription),
                    new XElement(rssFeedNamespace + "guid", new XAttribute("isPermaLink", "false"), page.Key.ToString("n")),
                    page.SeoThumbnail != null ? new XElement(rssFeedNamespace + "image", new Uri(viewService.CurrentUri, page.SeoThumbnail?.GetCropUrl(EditorImageCrops.Card))) : null);

                if (includeDecendants)
                {
                    foreach (var child in Pages(page, includeDecendants))
                    {
                        yield return child;
                    }
                }
            }
        }
    }

    private bool SeoEnabled(EditorPage? source)
    {
        var node = source;
        while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
        {
            if (node?.SeoStatus == SeoStatuses.Inherit)
            {
                node = node?.Parent() as EditorPage;
            }
            else
            {
                break;
            }
        }

        return node?.SeoStatus == SeoStatuses.Enable;
    }

    private string RssFeed(RssFeedEntryTab entry)
    {
        var sb = new StringBuilder();
        var xws = new XmlWriterSettings();
        xws.OmitXmlDeclaration = false;
        xws.Indent = true;
            
        using (XmlWriter xw = XmlWriter.Create(sb, xws))
        {
            var xDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                    new XElement(rssFeedNamespace + "rss", new XAttribute("version", "2.0"),
                    new XElement(rssFeedNamespace + "channel",
                        new XElement(rssFeedNamespace + "title", entry.Title),
                        new XElement(rssFeedNamespace + "description", entry.Description),
                        new XElement(rssFeedNamespace + "link", new Uri(viewService.CurrentUri, entry.Source?.Url())),
                        new XElement(rssFeedNamespace + "ttl", entry.Cache),
                        SeoEnabled(entry.Source as EditorPage) ? Pages(entry.Source, entry.IncludeAllDescendants) : null)));
            xDoc.Save(xw);
        }

        return sb.ToString();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (runtimeState.Level < RuntimeLevel.Run 
            || context.Request.Path.HasValue == false 
            || string.IsNullOrWhiteSpace(context.Request.Path.Value)
            || context.Request.IsBackOfficeRequest()
            || context.Request.HasPreviewCookie()
            || context.Request.IsUmbracoUrl()
            || context.Request.Path.Value.EndsWith(".rss", StringComparison.InvariantCultureIgnoreCase) == false)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
#if DEBUG
                // If the line below crashes with: "No controller/action found by name Render.Index"
                // Need to clean then rebuild solution
                throw;
#endif
            }
            return;
        }

        var rssFeedEntry = GetRssFeedEntry(viewService.CurrentDomainPage, context.Request.Path);
        if (rssFeedEntry == null) 
        {
            await next.Invoke(context);
            return;
        }

        var bytes = fusionCache.GetOrSet<byte[]>(nameof(RssFeedEntryTab) + context.Request.GetDisplayUrl(), _ =>
        {
            return Encoding.ASCII.GetBytes(RssFeed(rssFeedEntry));
        }, new TimeSpan(TimeSpan.TicksPerMinute * (((long) rssFeedEntry.Cache) + 1L)));

        context.Response.Headers.ContentType = "application/rss+xml";
        if (bytes != null)
        {
            context.Response.Body.Write(bytes, 0, bytes.Length);
        }
        context.Response.Body.Flush();
        //await next(context);
    }
}
