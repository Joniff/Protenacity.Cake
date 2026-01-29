using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Coroner;
using Protenacity.Cake.Web.Presentation.View;
using Protenacity.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Editor.Table;

public class TableViewComponent(IEditorService editorService,
    ISpreadsheetService spreadsheetService, 
    MediaFileManager mediaFileManager,
    IFusionCache fusionCache,
    IServiceProvider serviceProvider,
    IViewService viewService,
    ILogger<TableViewComponent> logger)
    : ViewComponent
{
    public const string Name = nameof(Table);
    public const string TemplateBlocks = "~/Views/Components/" + Name + "/Blocks.cshtml";
    public const string TemplateSimple = "~/Views/Components/" + Name + "/Simple.cshtml";
    public const string TemplateInquestHearing = "~/Views/Components/" + Name + "/InquestHearing.cshtml";
    public const string TemplateInquestOpening = "~/Views/Components/" + Name + "/InquestOpening.cshtml";
    public const string TemplateInquestConclusion = "~/Views/Components/" + Name + "/InquestConclusion.cshtml";
    public const string QueryString = "cq";

    private char Separator(EditorTableSourceFileSeparators? editorTableSourceFileSeparator)
    {
        switch (editorTableSourceFileSeparator)
        {
            case EditorTableSourceFileSeparators.Tab:
                return '\t';

            case EditorTableSourceFileSeparators.Semicolon:
                return ';';

            case EditorTableSourceFileSeparators.Pipe:
                return '|';
        }

        return ',';
    }

    private IViewComponentResult ReadText(EditorTableSourceText content, IEditorTableEmbeddedSettings? settings)
    {
        var text = content?.Contents;
        if (string.IsNullOrWhiteSpace(text))
        {
            return Content(string.Empty);
        }

        var data = spreadsheetService.ReadCsv(text, Separator(content?.SeparatorTyped));

        return View(TemplateSimple, new TableSimpleViewModel
        {
            QueryString = QueryString,
            Id = "Text-" + Guid.NewGuid().ToString("n"),
            Cells = data.Select((d, i) => new Tuple<int, string[]>(i, d)),
            AlternateBackgroundRows = settings?.AlternateBackgroundRows?.Color,
            AlternateBackgroundColumns = settings?.AlternateBackgroundColumns?.Color,
            FirstRowIsHeader = settings?.FirstRowIsHeader ?? false,
            FirstColumnIsHeader = settings?.FirstColumnIsHeader ?? false,
        });
    }

    private IViewComponentResult ReadFile(EditorTableSourceFile content, IEditorTableEmbeddedSettings? settings)
    {
        var file = content?.File?.Url();

        if (string.IsNullOrWhiteSpace(file))
        {
            return Content(string.Empty);
        }

        var data = fusionCache.GetOrSet<IEnumerable<string[]>>(nameof(TableViewComponent) + ":" + file, _ =>
        {
            using (var stream = mediaFileManager.FileSystem.OpenFile(file))
            {
                return spreadsheetService.Read(stream, Separator(content?.SeparatorTyped));
            }
        }, TimeSpan.FromHours(1));

        return View(TemplateSimple, new TableSimpleViewModel
        {
            QueryString = QueryString,
            Id = "File-" + Guid.NewGuid().ToString("n"),
            Cells = data.Select((d, i) => new Tuple<int, string[]>(i, d)),
            AlternateBackgroundRows = settings?.AlternateBackgroundRows?.Color,
            AlternateBackgroundColumns = settings?.AlternateBackgroundColumns?.Color,
            FirstRowIsHeader = settings?.FirstRowIsHeader ?? false,
            FirstColumnIsHeader = settings?.FirstColumnIsHeader ?? false
        });
    }

    public IEnumerable<T> Page<T>(IEnumerable<T>? list, int? page, int? pageSize) 
        => list == null ? Enumerable.Empty<T>() : ((page == null || pageSize == null) ? list : list.Skip(page.Value * pageSize.Value).Take(pageSize.Value));

    private IEnumerable<ICoronerInquestHearing> ReadCoronerHearing(IPublishedContent api)
    {
        switch (api.ContentType.Alias)
        {
            case CoronerDataVersion1.ModelTypeAlias:
                var apiV1 = api as CoronerDataVersion1 ?? throw new ArgumentException();
                return serviceProvider.GetKeyedService<Coroner.Version1.ICoronerService>(nameof(Coroner.Version1))?.GetInquestHearing(apiV1.Endpoint ?? "", apiV1.Authentication ?? "")
                    ?? Enumerable.Empty<ICoronerInquestHearing>();

            case CoronerDataVersion2.ModelTypeAlias:
                var apiV2 = api as CoronerDataVersion2 ?? throw new ArgumentException();
                return serviceProvider.GetKeyedService<Coroner.Version2.ICoronerService>(nameof(Coroner.Version2))?.GetInquestHearing(apiV2.Endpoint ?? "")
                    ?? Enumerable.Empty<ICoronerInquestHearing>();

            default:
                throw new ArgumentException("Unvalid type " + api.ContentType.Alias);
        }
    }

    private IEnumerable<ICoronerInquestOpening> ReadCoronerOpening(IPublishedContent api)
    {
        switch (api.ContentType.Alias)
        {
            case CoronerDataVersion1.ModelTypeAlias:
                var apiV1 = api as CoronerDataVersion1 ?? throw new ArgumentException();
                return serviceProvider.GetKeyedService<Coroner.Version1.ICoronerService>(nameof(Coroner.Version1))?.GetInquestOpening(apiV1.Endpoint ?? "", apiV1.Authentication ?? "")
                    ?? Enumerable.Empty<ICoronerInquestOpening>();

            case CoronerDataVersion2.ModelTypeAlias:
                var apiV2 = api as CoronerDataVersion2 ?? throw new ArgumentException();
                return serviceProvider.GetKeyedService<Coroner.Version2.ICoronerService>(nameof(Coroner.Version2))?.GetInquestOpening(apiV2.Endpoint ?? "")
                    ?? Enumerable.Empty<ICoronerInquestOpening>();

            default:
                throw new ArgumentException("Unvalid type " + api.ContentType.Alias);
        }
    }

    private IEnumerable<ICoronerInquestConclusion> ReadCoronerConclusion(IPublishedContent api)
    {
        switch (api.ContentType.Alias)
        {
            case CoronerDataVersion1.ModelTypeAlias:
                var apiV1 = api as CoronerDataVersion1 ?? throw new ArgumentException();
                return serviceProvider.GetKeyedService<Coroner.Version1.ICoronerService>(nameof(Coroner.Version1))?.GetInquestConclusion(apiV1.Endpoint ?? "", apiV1.Authentication ?? "")
                    ?? Enumerable.Empty<ICoronerInquestConclusion>();

            case CoronerDataVersion2.ModelTypeAlias:
                var apiV2 = api as CoronerDataVersion2 ?? throw new ArgumentException();
                return serviceProvider.GetKeyedService<Coroner.Version2.ICoronerService>(nameof(Coroner.Version2))?.GetInquestConclusion(apiV2.Endpoint ?? "")
                    ?? Enumerable.Empty<ICoronerInquestConclusion>();

            default:
                throw new ArgumentException("Unvalid type " + api.ContentType.Alias);
        }
    }

    IViewComponentResult ReadCoroner(EditorTableSourceCoronerInquestApi content, IEditorTableEmbeddedSettings? settings)
    {
        var id = "coroner-" + content.StageTyped.ToString().ToLowerInvariant();
        var alternateBackgroundRows = settings?.AlternateBackgroundRows;
        var alternateBackgroundColumns = settings?.AlternateBackgroundColumns;
        var firstRowIsHeader = settings?.FirstRowIsHeader ?? false;
        var firstColumnIsHeader = settings?.FirstColumnIsHeader ?? false;
        var customDateFormat = viewService.CurrentDomainPage.ConfigCoronerDateFormat ?? "d";
        var customTimeFormat = viewService.CurrentDomainPage.ConfigCoronerTimeFormat ?? "t";
        var page = viewService.CurrentSearchPage ?? 0;
        var pageSize = settings?.PageSize > 0 ? settings?.PageSize : null;
        var criteria = viewService.CurrentSearchCriteria(QueryString);

        if (content.Api == null || !content.Api.IsVisible())
        {
            logger.LogWarning("Coroner's API invalid on page " + viewService.CurrentPage.Id);
            return Content("Invalid Api");
        }

        switch (content.StageTyped)
        {
            case EditorCoronerInquestApiStages.Hearing:
                var hearings = ReadCoronerHearing(content.Api);
                if (!string.IsNullOrEmpty(criteria))
                {
                    hearings = hearings?.Where(h => h.Fullname?.InvariantContains(criteria) == true);
                }

                return View(TemplateInquestHearing, new TableCoronerViewModel<ICoronerInquestHearing>
                {
                    Id = id,
                    Title = "Search Names",
                    Criteria = criteria,
                    QueryString = QueryString,
                    Rows = Page(hearings, page, pageSize),
                    AlternateBackgroundRows = alternateBackgroundRows?.Color,
                    AlternateBackgroundColumns = alternateBackgroundColumns?.Color,
                    FirstRowIsHeader = firstRowIsHeader,
                    FirstColumnIsHeader = firstColumnIsHeader,
                    CustomDateFormat = customDateFormat,
                    CustomTimeFormat = customTimeFormat,
                    TotalResults = hearings?.Count() ?? 0,
                    Page = page,
                    PageSize = pageSize
                });

            case EditorCoronerInquestApiStages.Opening:
                var openings = ReadCoronerOpening(content.Api);
                if (!string.IsNullOrEmpty(criteria))
                {
                    openings = openings?.Where(h => h.Fullname?.InvariantContains(criteria) == true);
                }
                return View(TemplateInquestOpening, new TableCoronerViewModel<ICoronerInquestOpening>
                {
                    Id = id,
                    Title = "Search Names",
                    Criteria = criteria,
                    QueryString = QueryString,
                    Rows = Page(openings, page, pageSize),
                    AlternateBackgroundRows = alternateBackgroundRows?.Color,
                    AlternateBackgroundColumns = alternateBackgroundColumns?.Color,
                    FirstRowIsHeader = firstRowIsHeader,
                    FirstColumnIsHeader = firstColumnIsHeader,
                    CustomDateFormat = customDateFormat,
                    CustomTimeFormat = customTimeFormat,
                    TotalResults = openings?.Count() ?? 0,
                    Page = page,
                    PageSize = pageSize
                });

            case EditorCoronerInquestApiStages.Conclusion:
                var conclusions = ReadCoronerConclusion(content.Api);
                if (!string.IsNullOrEmpty(criteria))
                {
                    conclusions = conclusions?.Where(h => h.Fullname?.InvariantContains(criteria) == true);
                }
                return View(TemplateInquestConclusion, new TableCoronerViewModel<ICoronerInquestConclusion>
                {
                    Id = id,
                    Title = "Search Names",
                    Criteria = criteria,
                    QueryString = QueryString,
                    Rows = Page(conclusions, page, pageSize),
                    AlternateBackgroundRows = alternateBackgroundRows?.Color,
                    AlternateBackgroundColumns = alternateBackgroundColumns?.Color,
                    FirstRowIsHeader = firstRowIsHeader,
                    FirstColumnIsHeader = firstColumnIsHeader,
                    CustomDateFormat = customDateFormat,
                    CustomTimeFormat = customTimeFormat,
                    TotalResults = conclusions?.Count() ?? 0,
                    Page = page,
                    PageSize = pageSize
                });

            default:
                throw new ArgumentException("Unknown Stage called " + content.Stage);
        }
    }

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var source = (content.Block?.Content as IEditorTableEmbedded)?.Source?.FirstOrDefault();

        if (source == null)
        {
            return Content(string.Empty);
        }

        var settings = content.Block?.Settings as IEditorTableEmbeddedSettings;
        switch (source.Content.ContentType.Alias)
        {
            case EditorTableSourceBlocks.ModelTypeAlias:
                var cells = editorService.Load(null, content.Defaults, ((EditorTableSourceBlocks)source.Content).ListBlocks);
                var blockSettings = source.Settings as EditorTableSourceBlocksSettings;

                if (cells?.Contents.Any() != true)
                {
                    // We have no panels to show
                    return Content(string.Empty);
                }

                return View(TemplateBlocks, new TableBlocksViewModel
                {
                    QueryString = QueryString,
                    Id = "Blocks-" + Guid.NewGuid().ToString("n"),
                    Rows = cells
                        .Contents
                        .Chunk(blockSettings?.Columns < 1 ? 6 : blockSettings?.Columns ?? 6)
                        .Select((r, i) => new Tuple<int, IEnumerable<IEditorContent>>(i, r)),
                    AlternateBackgroundRows = settings?.AlternateBackgroundRows?.Color,
                    AlternateBackgroundColumns = settings?.AlternateBackgroundColumns?.Color,
                    FirstRowIsHeader = settings?.FirstRowIsHeader ?? false,
                    FirstColumnIsHeader = settings?.FirstColumnIsHeader ?? false
                });

            case EditorTableSourceText.ModelTypeAlias:
                return ReadText((EditorTableSourceText)source.Content, settings);

            case EditorTableSourceFile.ModelTypeAlias:
                return ReadFile((EditorTableSourceFile)source.Content, settings);

            case EditorTableSourceCoronerInquestApi.ModelTypeAlias:
                return ReadCoroner((EditorTableSourceCoronerInquestApi)source.Content, settings);

            default:
                return Content(string.Empty);
        }
    }
}
