using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Umbraco.Cms.Core.IO;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Pages.Domain;

[ApiExplorerSettings(IgnoreApi = true)]
public class DomainFilesController(
    MediaFileManager mediaFileManager,
    IViewService viewService,
    IFusionCache fusionCache)
    : Controller
{
    private XNamespace sitemapNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
    private XNamespace sitemapXsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

    private readonly TimeSpan FavIconCacheLength = TimeSpan.FromMinutes(1);
    private readonly TimeSpan RobotsTxtCacheLength = TimeSpan.FromMinutes(1);
    private readonly TimeSpan SecurityTxtCacheLength = TimeSpan.FromMinutes(1);
    private readonly TimeSpan SiteMapCacheLength = TimeSpan.FromDays(1);


    [Route("/favicon.ico")]
    [Route("/css/img/favicon.ico")]
    public IActionResult FavIconIco()
    {
        var currentDomainPage = viewService.CurrentDomainPage;

        var buffer = fusionCache.GetOrSet<byte[]?>(nameof(FavIconIco) + currentDomainPage.Key.ToString(), _ =>
        {
            var prop = currentDomainPage?.Icon?.GetProperty(Umbraco.Cms.Core.Constants.Conventions.Media.File);
            if (prop?.HasValue() != true)
            {
                return null;
            }

            using (var stream = mediaFileManager.FileSystem.OpenFile(prop.GetValue() as string ?? "/"))
            {
                var content = new byte[stream.Length];
                stream.ReadExactly(content, 0, (int) stream.Length);
                return content;
            }
        }, FavIconCacheLength);

        return ((buffer?.Length ?? 0) != 0) ? File(buffer!, "image/x-icon") : NoContent();
    }

    [Route("/robots.txt")]
    public IActionResult RobotsTxt()
    {
        var currentDomainPage = viewService.CurrentDomainPage;

        return fusionCache.GetOrSet<IActionResult>(nameof(RobotsTxt) + currentDomainPage.Key.ToString(), _ =>
        {
            var txt = new StringBuilder();
            if (currentDomainPage?.EnableSitemapxml == true)
            {
                txt.Append("Sitemap: ");
                var url = new Uri(viewService.CurrentUri, "sitemap.xml");
                txt.AppendLine(url.ToString());
            }

            if (currentDomainPage?.EnableIndexing == false)
            {
                txt.AppendLine("User-agent: *");
                txt.AppendLine("Disallow: /");
            }

            txt.AppendLine(currentDomainPage?.RobotsTxt);

            return File(Encoding.ASCII.GetBytes(txt.ToString()), "text/plain");
        }, RobotsTxtCacheLength) ?? NoContent();
    }

    private IEnumerable<XElement> Pages(DomainPage domain)
    {
        var currentUri = viewService.CurrentUri;
        foreach (var page in domain.DescendantsOrSelf<EditorPage>())
        {
            var pageHasBeenUpdatedInLastTwoMonths = (DateTime.Now - page.UpdateDate).TotalDays < 60.0;

            yield return new XElement(sitemapNamespace + "url",
                new XElement(sitemapNamespace + "loc", new Uri(currentUri, page.Url())),
                new XElement(sitemapNamespace + "lastmod", page.UpdateDate.ToUniversalTime().ToString("yyyy-MM-dd")),
                new XElement(sitemapNamespace + "changefreq", pageHasBeenUpdatedInLastTwoMonths ? "weekly" : "monthly"),
                new XElement(sitemapNamespace + "priority", page.SeoPriority == 0 ? 5 : page.SeoPriority));
        }
    }

    [Route("/sitemap.xml")]
    public IActionResult SitemapXml()
    {
        var currentDomainPage = viewService.CurrentDomainPage;

        return fusionCache.GetOrSet<IActionResult>(nameof(SitemapXml) + currentDomainPage.Key.ToString(), _ =>
        {
            var sb = new StringBuilder();
            var xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = false;
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(sb, xws))
            {
                var xDoc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "no"),
                        new XElement(sitemapNamespace + "urlset",
                            new XAttribute(XNamespace.Xmlns + "xsi", sitemapXsiNamespace),
                            new XAttribute(sitemapXsiNamespace + "schemaLocation", sitemapNamespace.NamespaceName + " " + sitemapNamespace.NamespaceName + "/sitemap.xsd"),
                        Pages(currentDomainPage)));
                xDoc.Save(xw);
            }
            return File(Encoding.ASCII.GetBytes(sb.ToString()), "application/xml");
        }, SiteMapCacheLength) ?? NoContent();
    }


    [Route("/security.txt")]
    [Route("/.well-known/security.txt")]
    public IActionResult SecurityTxt()
    {
        var currentDomainPage = viewService.CurrentDomainPage;

        return fusionCache.GetOrSet<IActionResult>(nameof(SecurityTxt) + currentDomainPage.Key.ToString(), _ =>
        {
            var txt = new StringBuilder();
            if (!currentDomainPage.SecurityEnable)
            {
                return NotFound();
            }

            txt.Append("Expires: ");
            txt.AppendLine(new DateTime(DateTime.UtcNow.AddMonths(18).Year, 1, 1).ToString("yyyy-MM-dd'T'HH:mm:ss'z'", CultureInfo.InvariantCulture));

            var currentUri = viewService.CurrentUri;

            if (!string.IsNullOrWhiteSpace(currentDomainPage.SecurityContact?.Url))
            {
                txt.Append("Contact: ");
                if (currentDomainPage.SecurityContact.Url.Contains(':'))
                {
                    txt.AppendLine(currentDomainPage.SecurityContact?.Url);
                }
                else
                {
                    var url = new Uri(currentUri, currentDomainPage.SecurityContact.Url);
                    txt.AppendLine(url.ToString());
                }
            }

            if (!string.IsNullOrWhiteSpace(currentDomainPage.SecurityPolicy?.Url))
            {
                txt.Append("Policy: ");
                var url = new Uri(currentUri, currentDomainPage.SecurityPolicy.Url);
                txt.AppendLine(url.ToString());
            }

            if (!string.IsNullOrWhiteSpace(currentDomainPage.SecurityAcknowledgments?.Url))
            {
                txt.Append("Acknowledgments: ");
                var url = new Uri(currentUri, currentDomainPage.SecurityAcknowledgments.Url);
                txt.AppendLine(url.ToString());
            }

            if (!string.IsNullOrWhiteSpace(currentDomainPage.SecurityHiring?.Url))
            {
                txt.Append("Hiring: ");
                if (currentDomainPage.SecurityHiring.Url.Contains(':'))
                {
                    txt.AppendLine(currentDomainPage.SecurityHiring.Url);
                }
                else
                {

                    var url = new Uri(currentUri, currentDomainPage.SecurityHiring?.Url);
                    txt.AppendLine(url.ToString());
                }
            }

            return File(Encoding.ASCII.GetBytes(txt.ToString()), "text/plain");
        }, SecurityTxtCacheLength) ?? NoContent();
    }
}
