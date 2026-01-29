using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Text;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.View;
public class ViewService(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoContextFactory umbracoContextFactory,
    IDocumentNavigationQueryService documentNavigationQueryService,
    IHttpContextAccessor httpContextAccessor,
    ILogger<ViewService> logger,
    IDocumentUrlService documentUrlService)
    : IViewService
{
    private bool umbracoContextFlag = false;
    private IUmbracoContext? umbracoContext = null;
    private IUmbracoContext? UmbracoContext
    {
        get
        {
            if (!umbracoContextFlag)
            {
                umbracoContext = umbracoContextAccessor.TryGetUmbracoContext(out var localUmbracoContext) ? localUmbracoContext : null;
                umbracoContextFlag = true;
            }
            return umbracoContext;
        }
    }

    private IPublishedContent? currentPage;
    public IPublishedContent CurrentPage
    {
        get
        {
            if (currentPage == null)
            {
                currentPage = UmbracoContext?.PublishedRequest?.PublishedContent
                    ?? throw new ApplicationException("Can only be called from within an Umbraco Frontend Request");
            }
            return currentPage;
        }
    }

    private DomainPage DomainPageWhenOutsideUmbracoPage
    {
        get
        {
            var umbracoContextReference = umbracoContextFactory.EnsureUmbracoContext()
                ?? throw new ApplicationException("Unable to create an " + nameof(Umbraco.Cms.Core.UmbracoContextReference));
            var umbracoContext = umbracoContextReference.UmbracoContext
                ?? throw new ApplicationException("Unable to find the " + nameof(Umbraco.Cms.Web.Common.UmbracoContext));
            var domain = DomainUtilities.SelectDomain(umbracoContext.Domains?.GetAll(false), umbracoContext.CleanedUmbracoUrl);
            var content = umbracoContext.Content?.GetById(domain?.ContentId ?? Constants.System.Root)
                ?? (documentNavigationQueryService.TryGetRootKeys(out var roots) ? (roots?.Any() == true ? umbracoContext.Content?.GetById(roots.First()) : null) : null)
                ?? throw new ApplicationException("Can\'t find any published content");
            return content.AncestorOrSelf<DomainPage>()
                ?? throw new ApplicationException($"{content.Name}({content.Id}) page doesn\'t have a {DomainPage.ModelTypeAlias} as an ancestor");
        }
    }

    private DomainPage? currentDomainPage = null;
    public DomainPage CurrentDomainPage
    {
        get
        {
            if (currentDomainPage == null)
            {
                var currentPage = UmbracoContext?.PublishedRequest?.PublishedContent;
                currentDomainPage = (currentPage != null
                    ? CurrentPage.AncestorOrSelf<DomainPage>()
                    : DomainPageWhenOutsideUmbracoPage)
                        ?? throw new ApplicationException("Unable to find a domain page");
            }
            return currentDomainPage;
        }
    }

    private Uri? currentDomainUri;
    public Uri CurrentDomainUri
    {
        get
        {
            if (currentDomainUri == null)
            {
                currentDomainUri = new Uri(new Uri(httpContextAccessor?.HttpContext?.Request?.GetEncodedUrl() ?? throw new ApplicationException("Can only be run within an existing web request"))
                    .GetLeftPart(UriPartial.Authority));
            }
            return currentDomainUri;
        }
    }

    private Uri? currentUri;
    public Uri CurrentUri
    {
        get
        {
            if (currentUri == null)
            {
                var currentPage = UmbracoContext?.PublishedRequest?.PublishedContent;
                currentUri = new Uri(CurrentDomainUri, currentPage?.Url() ?? httpContextAccessor?.HttpContext?.Request.GetDisplayUrl());
            }
            return currentUri;
        }
    }

    private Uri? canonicalUri;

    public Uri CanonicalUri
    {
        get
        {
            if (canonicalUri == null)
            {
                var seo = CurrentPage as ISeoTab;
                canonicalUri = CurrentUri;
                if (!string.IsNullOrWhiteSpace(seo?.SeoCanonicalUrl?.Url))
                {
                    if (Uri.TryCreate(seo.SeoCanonicalUrl.Url, UriKind.Absolute, out var absolute))
                    {
                        canonicalUri = absolute;
                    }
                    else if (Uri.TryCreate(CurrentDomainUri, seo.SeoCanonicalUrl.Url, out var relative))
                    {
                        canonicalUri = relative;
                    }
                    else
                    {
                        logger.LogError($"{seo.SeoCanonicalUrl.Url} is an invalid Canonical Url on page {CurrentPage.Name} ({CurrentPage.Url()}). Check Seo Tab -> Canonical Url.");
                    }
                }
            }
            return canonicalUri;
        }
    }

    private bool haveTriedToGetFurniture = false;
    private IFurnitureTab? currentFurniture;
    private string? currentFurnitureUrl;

    private void SetFurniture()
    {
        haveTriedToGetFurniture = true;
        var node = CurrentPage as EditorPage;

        while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
        {
            switch (node.FurnitureStatusTyped)
            {
                case FurntitureStatuses.Inherit:
                    node = node?.Parent() as EditorPage;
                    break;

                case FurntitureStatuses.Show:
                    currentFurniture = node;
                    currentFurnitureUrl = node?.Url();
                    return;

                default:
                    throw new ApplicationException(node.FurnitureStatusTyped + " is invalid " + nameof(FurntitureStatuses));
            }
        }
        throw new ApplicationException("Home page has to have Furniture status set to Show");
    }

    public IFurnitureTab CurrentFurniture
    {
        get
        {
            if (!haveTriedToGetFurniture)
            {
                SetFurniture();
            }
            return currentFurniture ?? throw new ApplicationException(nameof(SetFurniture) + "() not working as expected");
        }
    }

    public string CurrentFurnitureUrl
    {
        get
        {
            if (!haveTriedToGetFurniture)
            {
                SetFurniture();
            }
            return currentFurnitureUrl ?? throw new ApplicationException(nameof(SetFurniture) + "() not working as expected");
        }
    }

    private IFurnitureTab? currentBreadcrumb;
    public IFurnitureTab CurrentBreadcrumb
    {
        get
        {
            if (currentBreadcrumb != null)
            {
                return currentBreadcrumb;
            }

            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.BreadcrumbStatusTyped)
                {
                    case BreadcrumbStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    default:
                        currentBreadcrumb = node;
                        return currentBreadcrumb;
                }
            }
            throw new ApplicationException("Home page has to have Breadcrumb status set to either show or hide");
        }
    }

    public string? Parse(string? text, IDictionary<string, string>? values = null)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var replace = new StringBuilder(text);

        if (values?.Any() == true)
        {
            foreach (var field in values)
            {
                replace.Replace(field.Key, field.Value);
            }
        }

        foreach (var field in StandardParseValues)
        {
            replace.Replace(field.Key, field.Value);
        }

        return replace.ToString();
    }

    public string? Parse(IHtmlEncodedString? text, IDictionary<string, string>? values = null) => Parse(text?.ToString(), values);

    public IDictionary<string, string> StandardParseValues => new Dictionary<string, string> {
        { RichTextFields.Year, DateTime.UtcNow.ToString("yyyy") },
        { RichTextFields.Month, DateTime.UtcNow.ToString("MMM") },
        { RichTextFields.Day, DateTime.UtcNow.ToString("dd") },
        { RichTextFields.Week, DateTime.UtcNow.ToString("dddd") },
        { RichTextFields.Now, DateTime.UtcNow.ToString("dd MMM yyyy") },
        { RichTextFields.Criteria, CurrentSearchCriteria(Protenacity.Cake.Web.Presentation.Editor.Search.SearchViewComponent.QueryString) ?? ""},
        { RichTextFields.Category, string.Join(", ", ((CurrentPage) as EditorPage)?.Categories?.Select(c => c.Name) ?? Enumerable.Empty<string>()) },
        { RichTextFields.CategoryHeading, string.Join(", ", ((CurrentPage) as EditorPage)?.Categories?.Select(c => c.Parent()?.Name) ?? Enumerable.Empty<string>()) }
    };

    private bool haveTriedToGetAlert = false;
    private IAlertTab? currentAlert = null;
    public IAlertTab? CurrentAlert
    {
        get
        {
            if (haveTriedToGetAlert)
            {
                return currentAlert;
            }

            haveTriedToGetAlert = true;
            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.AlertStatusTyped)
                {
                    case AlertStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    case AlertStatuses.Hide:
                        return null;

                    case AlertStatuses.Show:
                        currentAlert = node;
                        return node;

                    default:
                        throw new ApplicationException(node.AlertStatusTyped + " is invalid " + nameof(AlertStatuses));
                }
            }
            return null;
        }
    }

    private bool haveTriedToGetAside = false;
    private IAsideTab? currentAside = null;
    public IAsideTab? CurrentAside
    {
        get
        {
            if (haveTriedToGetAside)
            {
                return currentAside;
            }

            haveTriedToGetAside = true;
            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.AsideStatusTyped)
                {
                    case AsideStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    case AsideStatuses.Hide:
                        return null;

                    case AsideStatuses.Left:
                    case AsideStatuses.Right:
                        currentAside = node;
                        return node;

                    default:
                        throw new ApplicationException(node.AsideStatusTyped + " is invalid " + nameof(AsideStatuses));
                }
            }
            return null;
        }
    }

    private ISeoViewModel? currentSeo = null;
    public ISeoViewModel CurrentSeo
    {
        get
        {
            if (currentSeo != null)
            {
                return currentSeo;
            }

            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.SeoStatusTyped)
                {
                    case SeoStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    case SeoStatuses.Enable:
                    case SeoStatuses.Disable:
                        var content = (OriginalContent as ISeoTab)?.SeoStatusTyped == SeoStatuses.Enable || (OriginalContent as ISeoTab)?.SeoStatusTyped == SeoStatuses.Disable ? OriginalContent! : CurrentPage;
                        var seo = content as ISeoTab;

                        DateTime date = content.UpdateDate;
                        if (seo != null && seo.SeoDatePublished != DateTime.MinValue)
                        {
                            date = seo.SeoDatePublished;
                        }

                        if (node.SeoDatePublished != DateTime.MinValue)
                        {
                            date = node.SeoDatePublished;
                        }

                        currentSeo = new SeoViewModel
                        {
                            Enable = node.SeoStatusTyped == SeoStatuses.Enable,
                            Keywords = seo?.Keywords?.Any() == true ? seo.Keywords : node.Keywords,
                            DatePublished = date,
                            Description = string.IsNullOrWhiteSpace(seo?.SeoDescription) ? node.SeoDescription : seo.SeoDescription,
                            Priority = seo?.SeoPriority > 0 ? seo.SeoPriority : node.SeoPriority,
                            Title = string.IsNullOrWhiteSpace(seo?.Title) ? content.Name : seo.Title,
                            Thumbnail = seo?.SeoThumbnail == null ? node.SeoThumbnail : seo.SeoThumbnail
                        };
                        return currentSeo;

                    default:
                        throw new ApplicationException(node.SeoStatusTyped + " is invalid " + nameof(SeoStatuses));
                }
            }

            throw new ApplicationException("Home page has to have Seo status set to either enable or disable");
        }
    }

    private bool haveTriedToGetBanner = false;
    private IBannerTab? currentBanner = null;
    public IBannerTab? CurrentBanner
    {
        get
        {
            if (haveTriedToGetBanner)
            {
                return currentBanner;
            }

            haveTriedToGetBanner = true;
            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.BannerStatusTyped)
                {
                    case BannerStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    case BannerStatuses.Hide:
                        return null;

                    case BannerStatuses.ShowBanners:
                    case BannerStatuses.ShowImage:
                        currentBanner = node;
                        return node;

                    default:
                        throw new ApplicationException(node.BannerStatusTyped + " is invalid " + nameof(BannerStatuses));
                }
            }
            return null;
        }
    }

    private bool haveTriedToGetSubfooter = false;
    private ISubfooterTab? currentSubfooter = null;
    public ISubfooterTab? CurrentSubfooter
    {
        get
        {
            if (haveTriedToGetSubfooter)
            {
                return currentSubfooter;
            }

            haveTriedToGetSubfooter = true;
            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.SubfooterStatusTyped)
                {
                    case SubfooterStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    case SubfooterStatuses.Hide:
                        return null;

                    case SubfooterStatuses.Show:
                        currentSubfooter = node;
                        return node;

                    default:
                        throw new ApplicationException(node.SubfooterStatusTyped + " is invalid " + nameof(SubfooterStatuses));
                }
            }
            return null;
        }
    }

    private bool haveTriedToGetVirtualAgent = false;
    private VirtualAgentData? currentVirtualAgent = null;
    public VirtualAgentData? CurrentVirtualAgent
    {
        get
        {
            if (haveTriedToGetVirtualAgent)
            {
                return currentVirtualAgent;
            }

            haveTriedToGetVirtualAgent = true;
            var node = CurrentPage as EditorPage;

            while (node != null && node.ContentType.Alias == EditorPage.ModelTypeAlias)
            {
                switch (node.VirtualAgentStatusTyped)
                {
                    case VirtualAgentStatuses.Inherit:
                        node = node?.Parent() as EditorPage;
                        break;

                    case VirtualAgentStatuses.Disable:
                        return null;

                    case VirtualAgentStatuses.Enable:
                        if (node.VirtualAgentInstall == null)
                        {
                            return null;
                        }

                        currentVirtualAgent = node.VirtualAgentInstall as VirtualAgentData;
                        return currentVirtualAgent;

                    default:
                        throw new ApplicationException(node.VirtualAgentStatusTyped + " is invalid " + nameof(VirtualAgentStatuses));
                }
            }
            return null;
        }
    }

    public string? CurrentSearchCriteria(string queryString)
    {
        var query = httpContextAccessor?.HttpContext?.Request?.Query[queryString];
        if (query.HasValue && !string.IsNullOrWhiteSpace(query.Value))
        {
            return string.Join(' ', query.Value.ToList());
        }
        return null;
    }

    public int? CurrentSearchPage => int.TryParse(httpContextAccessor?.HttpContext?.Request?.Query["p"].FirstOrDefault(), out var page) ? page : null;

    public int? CurrentSearchPageSize => int.TryParse(httpContextAccessor?.HttpContext?.Request?.Query["s"].FirstOrDefault(), out var pageSize) ? pageSize : null;


    private EditorThemes? currentTheme = null;
    public EditorThemes CurrentTheme
    {
        get
        {
            if (currentTheme == null)
            {
                var current = CurrentPage as EditorPage;
                while (current != null && current.IsDocumentType(EditorPage.ModelTypeAlias) && current.ThemeTyped == EditorThemes.Inherit)
                {
                    current = current?.Parent() as EditorPage;
                }
                currentTheme = current?.ThemeTyped ?? EditorThemes.Venice;
            }
            return currentTheme ?? EditorThemes.Venice;
        }
    }

    private EditorSubthemes? currentSubtheme = null;
    public EditorSubthemes CurrentSubtheme
    {
        get
        {
            if (currentSubtheme == null)
            {
                var current = CurrentPage as EditorPage;
                while (current != null && current.IsDocumentType(EditorPage.ModelTypeAlias) && current.SubthemeTyped == EditorSubthemes.Inherit)
                {
                    current = current?.Parent() as EditorPage;
                }
                currentSubtheme = current?.SubthemeTyped ?? EditorSubthemes.Primary;
            }
            return currentSubtheme ?? EditorSubthemes.Primary;
        }
    }

    private EditorThemeShades? currentThemeShade = null;
    public EditorThemeShades CurrentThemeShade
    {
        get
        {
            if (currentThemeShade == null)
            {
                var current = CurrentPage as EditorPage;
                while (current != null && current.IsDocumentType(EditorPage.ModelTypeAlias) && current.ThemeShadeTyped == EditorThemeShades.Inherit)
                {
                    current = current?.Parent() as EditorPage;
                }
                currentThemeShade = current?.ThemeShadeTyped ?? EditorThemeShades.Light;
            }
            return currentThemeShade ?? EditorThemeShades.Light;
        }
    }

    private string JsonFields(object? value, Type type)
    {
        var json = new StringBuilder();
        var fieldCount = 0;
        json.Append('{');

        var props = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)
            .Union(type.GetAllProperties().Where(p => p.PropertyType == typeof(Protenacity.Cake.Web.Core.Constitution.CookieConsentCookie)));

        foreach (var prop in props)
        {
            if (fieldCount++ != 0)
            {
                json.Append(',');
            }
            json.Append(JsonField(prop.Name, prop.GetValue(value)));
        }
        json.Append('}');
        return json.ToString();
    }

    private string JsonField(string? name, object? value)
    {
        var json = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(name))
        {
            json.Append('\"');
            json.Append(System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(name.StartsWith("CookieConsent") ? name.Substring(13) : name));
            json.Append("\":");
        }

        switch (value)
        {
            case null:
                return "";

            case string stringValue:
                json.Append('\"');
                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    json.Append(System.Web.HttpUtility.HtmlAttributeEncode(stringValue));
                }
                json.Append('\"');
                break;

            case IEnumerable<string> stringValues:
                {
                    var fieldCount = 0;
                    json.Append('[');
                    foreach (var item in stringValues)
                    {
                        if (fieldCount++ != 0)
                        {
                            json.Append(',');
                        }
                        json.Append('\"');
                        json.Append(System.Web.HttpUtility.HtmlAttributeEncode(item));
                        json.Append('\"');
                    }
                    json.Append(']');
                }
                break;

            case int intValue:
                json.Append(intValue);
                break;

            case bool boolValue:
                json.Append(boolValue ? "true" : "false");
                break;

            case IHtmlEncodedString htmlValue:
                json.Append('\"');
                json.Append(System.Web.HttpUtility.HtmlEncode(htmlValue.ToString()));
                json.Append('\"');
                break;

            default:
                var arrayValue = value as IEnumerable<object>;
                if (arrayValue != null)
                {
                    var fieldCount = 0;
                    json.Append('[');
                    foreach (var item in arrayValue)
                    {
                        if (fieldCount++ != 0)
                        {
                            json.Append(',');
                        }
                        json.Append(JsonFields(item, item.GetType()));
                    }
                    json.Append(']');
                }
                else
                {
                    json.Append(JsonFields(value, value.GetType()));
                }
                break;
        }
        return json.ToString();
    }

    public string CurrentCookies => System.Web.HttpUtility.HtmlAttributeEncode(JsonFields(CurrentDomainPage, typeof(ICookieConsentTab)));

    private IPublishedContent? originalContent;
    private bool originalContentAttempt = false;

    public IPublishedContent? OriginalContent 
    {
        get
        {
            if (originalContent == null && originalContentAttempt == false)
            {
                originalContentAttempt = true;
                if (UmbracoContext?.PublishedRequest?.IsInternalRedirect == true && UmbracoContext?.OriginalRequestUrl != null)
                {
                    var path = UmbracoContext.OriginalRequestUrl.ToString().Substring(UmbracoContext?.PublishedRequest?.Domain?.Uri.ToString().Length ?? 0);
                    var key = documentUrlService.GetDocumentKeyByRoute(path, UmbracoContext?.PublishedRequest?.Culture, UmbracoContext?.PublishedRequest?.Domain?.ContentId, false);
                    if (key != null)
                    {
                        originalContent = UmbracoContext?.Content.GetById((Guid) key);
                    }
                }
                else if (httpContextAccessor?.HttpContext?.Request?.Cookies.TryGetValue(nameof(OriginalContent), out var text) == true && int.TryParse(text, out var id) == true)
                {
                    originalContent = UmbracoContext?.Content.GetById(id);
                    httpContextAccessor?.HttpContext?.Response.Cookies.Delete(nameof(OriginalContent));
                }
            }
            return originalContent;
        }
        set
        {
            originalContent = value;
            if (value == null)
            {
                httpContextAccessor?.HttpContext?.Response.Cookies.Delete(nameof(OriginalContent));
            }
            else
            {
                httpContextAccessor?.HttpContext?.Response.Cookies.Append(nameof(OriginalContent), value.Id.ToString());
            }
        }
    }

    public BusRouteData? CurrentBusRouteData { get; set; }
}
