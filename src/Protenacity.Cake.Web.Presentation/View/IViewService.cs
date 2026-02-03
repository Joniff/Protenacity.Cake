using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.View;
public interface IViewService
{
    IPublishedContent CurrentPage { get; }
    DomainPage CurrentDomainPage { get; }
    Uri CurrentDomainUri { get; }
    Uri CurrentUri { get; }
    Uri CanonicalUri { get; }
    IFurnitureTab CurrentFurniture { get; }
    string CurrentFurnitureUrl { get; }
    IFurnitureTab CurrentBreadcrumb { get; }
    string? Parse(string? text, IDictionary<string, string>? values = null);
    string? Parse(IHtmlEncodedString? text, IDictionary<string, string>? values = null);
    IDictionary<string, string> StandardParseValues { get; }
    IAlertTab? CurrentAlert { get; }
    IAsideTab? CurrentAside { get; }
    ISeoViewModel CurrentSeo { get; }
    IBannerTab? CurrentBanner { get; }
    ISubfooterTab? CurrentSubfooter { get; }
    VirtualAgentData? CurrentVirtualAgent { get; }
    string? CurrentSearchCriteria(string queryString);
    int? CurrentSearchPage { get; }
    int? CurrentSearchPageSize { get; }
    EditorThemes CurrentTheme { get; }
    EditorSubthemes CurrentSubtheme { get; }
    EditorThemeShades CurrentThemeShade { get; }
    string CurrentCookies { get; }
    IPublishedContent? OriginalContent { get; set; }
}
