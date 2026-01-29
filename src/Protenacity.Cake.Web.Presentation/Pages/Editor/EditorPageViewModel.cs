using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Protenacity.Cake.Web.Presentation.Pages.Editor;

public class EditorPageViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) 
    : EditorPage(content, publishedValueFallback)
{
    public IEnumerable<Tuple<string, string>>? Breadcrumbs { get; init; }
    public EditorSubthemes BreadcrumbSubtheme { get; init; }
    public EditorThemeShades BreadcrumbThemeShade { get; init; }
    public BlockListModel? BreadcrumbsColor { get; init; }
    public EditorSubthemes? PageTitleSubtheme { get; init; }
    public EditorThemeShades? PageTitleThemeShade { get; init; }
    public IAlertTab? Alert { get; init; }
    public required IEnumerable<IEditorContent> Contents { get; init; }
    public Tuple<bool, IEnumerable<IEditorContent>>? AboveContents { get; init; }
    public Tuple<bool, IEnumerable<IEditorContent>>? BelowContents { get; init; }
    public AsideStatuses PageLayout { get; init; }
    public IEnumerable<IEditorContents>? AsideDesktop { get; init; }
    public IEnumerable<IEditorContents>? AsideMobile { get; init; }
    public ISubfooterTab? Subfooter { get; init; }
    public EditorThemes Theme { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades ThemeShade { get; init; }
}
