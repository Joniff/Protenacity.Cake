using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.Search;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Editor.Search;

public class SearchViewComponent(IViewService viewService, 
    IEditorSearchService editorSearchInternal, 
    IUmbracoHelperAccessor umbracoHelperAccessor) 
    : ThemeViewComponent
{
    public const string Name = nameof(Search);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";
    public const string QueryString = "q";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var criteria = viewService.CurrentSearchCriteria(QueryString);
        var page = viewService.CurrentSearchPage >= 0 && viewService.CurrentSearchPage < viewService.CurrentDomainPage.ConfigPageMaximum 
            ? viewService.CurrentSearchPage : 
            0;
        var pageSize = PageSize(content, viewService.CurrentDomainPage.ConfigPageSizeDefault, viewService.CurrentDomainPage.ConfigPageSizeMaximum);
        var results = !string.IsNullOrWhiteSpace(criteria) 
            ? editorSearchInternal.Search(viewService.CurrentDomainPage, criteria, page ?? 0, pageSize) 
            : null;
        if (!umbracoHelperAccessor.TryGetUmbracoHelper(out var umbracoHelper))
        {
            throw new ApplicationException("Can\'t get UmbracoHelper");
        }

        var settings = content.Block?.Settings as EditorSearchPrimarySettings;

        return View(new SearchViewModel
        {
            Id = content.Id,
            Title = "Search " + viewService.CurrentDomainPage.Title,
            Criteria = criteria,
            QueryString = QueryString,
            Page = results?.Page,
            PageSize = results?.PageSize,
            TotalResults = results?.TotalResults,
            DidYouMean = results?.DidYouMean,
            Results = results?.Results.Select(r => new SearchViewModel.Result
            {
                ContentId = r.ContentId,
                DomainId = r.DomainId,
                Body = settings?.ResultOutput == Core.Property.EditorSearchResultOutputs.Highlight ? r.Body : r.Abstract,
                Headers = r.Headers,
                Path = r.Path,
                Priority = r.Priority,
                Url = umbracoHelper.Content(r.ContentId)?.Url() ?? string.Empty,
                Categories = new Guid[0] 
            }),
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content)
        });
    }
}
