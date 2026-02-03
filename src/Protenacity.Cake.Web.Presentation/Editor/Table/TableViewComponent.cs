using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
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

            default:
                return Content(string.Empty);
        }
    }
}
