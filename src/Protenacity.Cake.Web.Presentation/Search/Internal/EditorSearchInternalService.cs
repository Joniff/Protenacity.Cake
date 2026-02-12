using Examine;
using Microsoft.Extensions.Logging;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Presentation.Search.Core;
using System.Net;
using System.Text;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Search.Internal;

public class EditorSearchInternalService(
    ILogger<EditorSearchInternalService> logger,
    IJsonSerializer jsonSerializer,
    IPublishedContentTypeCache publishedContentTypeCache,
    IExamineManager examineManager)
    : IEditorSearchInternalService
{
    private static string DomainId => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.DomainId));
    private static string ContentId => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.ContentId));
    private static string Path => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Path));
    private static string[] Headers => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Headers)).Split(',');
    private static string Body => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Body));
    private static string Keywords => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Keywords));
    private static string Promoted => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Promoted));
    private static string Priority => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Priority));
    private static string Categories => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Categories));
    private static string Abstract => typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Abstract));
    private static string EditorPage_PageHeader => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.PageTitleName));
    private static string EditorPage_Title => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.Title));
    private static string EditorPage_Body => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.Body));
    private static string EditorPage_SeoPriority => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.SeoPriority));
    private static string EditorPage_Keywords => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.Keywords));
    private static string EditorPage_Promoted => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.SeoPromotedSearchTerms));
    private static string EditorPage_Categories => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.Categories));
    private static string EditorPage_Abstract => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.SeoAbstract));
    private static string EditorPage_Description => typeof(EditorPage).ModelsBuilderAlias(nameof(EditorPage.SeoDescription));
    private static string EditorBlockPrimary_Enable => typeof(EditorBlockPrimary).ModelsBuilderAlias(nameof(EditorBlockPrimary.Enable));
    private static string EditorBlockPrimary_MainTitle => typeof(EditorBlockPrimary).ModelsBuilderAlias(nameof(EditorBlockPrimary.MainTitle));
    private static string EditorPanelName_PanelName => typeof(EditorPanelName).ModelsBuilderAlias(nameof(EditorPanelName.PanelName));
    private static string EditorListEmbedded_ListBlocks => typeof(EditorListEmbedded).ModelsBuilderAlias(nameof(EditorListEmbedded.ListBlocks));
    private static string EditorPanel_PanelBlocks => typeof(EditorPanel).ModelsBuilderAlias(nameof(EditorPanel.PanelBlocks));
    private static string EditorTextEmbedded_Text => typeof(EditorTextEmbedded).ModelsBuilderAlias(nameof(EditorTextEmbedded.Text));
    private static string EditorNoteBase_Header => typeof(EditorCardBase).ModelsBuilderAlias(nameof(EditorCardBase.Header));
    private static string EditorNoteBase_Text => typeof(EditorCardBase).ModelsBuilderAlias(nameof(EditorCardBase.Text));
    private static string EditorStepperEmbedded_Steps => typeof(EditorStepperEmbedded).ModelsBuilderAlias(nameof(EditorStepperEmbedded.Steps));
    private static string EditorStep_Header => typeof(EditorStep).ModelsBuilderAlias(nameof(EditorStep.Header));
    private static string EditorStep_Text => typeof(EditorStep).ModelsBuilderAlias(nameof(EditorStep.Text));
    private static string EditorOpeningTimesEmbedded_Header => typeof(EditorOpeningTimesEmbedded).ModelsBuilderAlias(nameof(EditorOpeningTimesEmbedded.Header));
    private static string EditorOpeningTimesEmbedded_Text => typeof(EditorOpeningTimesEmbedded).ModelsBuilderAlias(nameof(EditorOpeningTimesEmbedded.Text));
    private static string EditorOpeningTimesEmbedded_Footer => typeof(EditorOpeningTimesEmbedded).ModelsBuilderAlias(nameof(EditorOpeningTimesEmbedded.Footer));
    private static string EditorVideoEmbedded_Header => typeof(EditorVideoEmbedded).ModelsBuilderAlias(nameof(EditorVideoEmbedded.Header));
    private static string EditorVideoEmbedded_Copyright => typeof(EditorVideoEmbedded).ModelsBuilderAlias(nameof(EditorVideoEmbedded.Copyright));

    private Lazy<Guid> EditorListPrimaryKey => new(() => EditorListPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorListPrimary) + " missing"));
    private Lazy<Guid> EditorListEmbeddedKey => new(() => EditorListEmbedded.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorListEmbedded) + " missing"));
    private Lazy<Guid> EditorListPanelKey => new(() => EditorListPanel.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorListPanel) + " missing"));
    private Lazy<Guid> EditorTabsPrimaryKey => new(() => EditorTabsPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorTabsPrimary) + " missing"));
    private Lazy<Guid> EditorAccordionPrimaryKey => new(() => EditorAccordionPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorAccordionPrimary) + " missing"));
    private Lazy<Guid> EditorTextPrimaryKey => new(() => EditorTextPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorTextPrimary) + " missing"));
    private Lazy<Guid> EditorTextEmbeddedKey => new(() => EditorTextEmbedded.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorTextEmbedded) + " missing"));
    private Lazy<Guid> EditorTextPanelKey => new(() => EditorTextPanel.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorTextPanel) + " missing"));
    private Lazy<Guid> EditorNotePrimaryKey => new(() => EditorCardPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorCardPrimary) + " missing"));
    private Lazy<Guid> EditorNoteEmbeddedKey => new(() => EditorCardEmbedded.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorCardEmbedded) + " missing"));
    private Lazy<Guid> EditorNotePanelKey => new(() => EditorCardPanel.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorCardPanel) + " missing"));
    private Lazy<Guid> EditorStepperEmbeddedKey => new(() => EditorStepperEmbedded.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorStepperEmbedded) + " missing"));
    private Lazy<Guid> EditorStepperPanelKey => new(() => EditorStepperPanel.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorStepperPanel) + " missing"));
    private Lazy<Guid> EditorStepperPrimaryKey => new(() => EditorStepperPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorStepperPrimary) + " missing"));
    private Lazy<Guid> EditorOpeningTimesEmbeddedKey => new(() => EditorOpeningTimesEmbedded.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorOpeningTimesEmbedded) + " missing"));
    private Lazy<Guid> EditorOpeningTimesPrimaryKey => new(() => EditorOpeningTimesPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorOpeningTimesPrimary) + " missing"));
    private Lazy<Guid> EditorOpeningTimesPanelKey => new(() => EditorOpeningTimesPanel.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorOpeningTimesPanel) + " missing"));
    private Lazy<Guid> EditorVideoPrimaryKey => new(() => EditorVideoPrimary.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorVideoPrimary) + " missing"));
    private Lazy<Guid> EditorVideoEmbeddedKey => new(() => EditorVideoEmbedded.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorVideoEmbedded) + " missing"));
    private Lazy<Guid> EditorVideoPanelKey => new(() => EditorVideoPanel.GetModelContentType(publishedContentTypeCache)?.Key ?? throw new ApplicationException(nameof(EditorVideoPanel) + " missing"));

    private IEnumerable<string> TextFields => Headers.Union(new string[] { Keywords, Promoted, Body });

    private IEnumerable<BlockItemData> Blocks(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return Enumerable.Empty<BlockItemData>();
        }
        var converter = new BlockListEditorDataConverter(jsonSerializer);
        var blocks = converter.Deserialize(json);
        return blocks?.BlockValue?.ContentData ?? Enumerable.Empty<BlockItemData>();
    }

    private string? RichTextField(BlockItemData block, string alias) =>
        RichTextPropertyEditorHelper.TryParseRichTextEditorValue(block.Value<string>(alias), jsonSerializer, logger, out var value)
        ? WebUtility.HtmlDecode(new HtmlEncodedString(value.Markup).ToText().ToString())
        : null;

    private string? RichTextField(IContent content, string alias) =>
        RichTextPropertyEditorHelper.TryParseRichTextEditorValue(content.GetValue<string>(alias), jsonSerializer, logger, out var value)
        ? WebUtility.HtmlDecode(new HtmlEncodedString(value.Markup).ToText().ToString())
        : null;

    private string? RichTextBlocks(BlockItemData block, string alias)
    {
        if (!RichTextPropertyEditorHelper.TryParseRichTextEditorValue(block.Value<string>(alias), jsonSerializer, logger, out var format) || format.Blocks == null)
        {
            return null;
        }
        var richBlocks = format.Blocks.ContentData;
        if (richBlocks?.Any() != true)
        {
            return null;
        }
        var results = new List<string>();

        foreach (var richBlock in richBlocks)
        {
            foreach (var value in richBlock.Values)
            {
                if (value.Alias == "text")
                {
                    if (RichTextPropertyEditorHelper.TryParseRichTextEditorValue(value.Value, jsonSerializer, logger, out var markup))
                    {
                        results.Add(WebUtility.HtmlDecode(new HtmlEncodedString(markup.Markup).ToText().ToString() ?? string.Empty));
                    }
                }
            }
        }
        return string.Join(' ', results);
    }

    private void DesiccateChildren(BlockItemData block, string alias, ref List<string> h3, ref List<string> body)
    {
        var blocks = Blocks(block.Value<string>(alias));
        if (blocks?.Any() == true)
        {
            foreach (var childBlock in blocks)
            {
                Desiccate(childBlock, ref h3, ref body);
            }
        }
    }

    private void Desiccate(BlockItemData block, ref List<string> h3, ref List<string> body)
    {
        var panelName = RichTextField(block, EditorPanelName_PanelName);
        if (!string.IsNullOrWhiteSpace(panelName))
        {
            h3.Add(panelName);
            body.Add(panelName);
        }

        if (block.ContentTypeKey == EditorListPrimaryKey.Value ||
            block.ContentTypeKey == EditorListEmbeddedKey.Value ||
            block.ContentTypeKey == EditorListPanelKey.Value)
        {
            DesiccateChildren(block, EditorListEmbedded_ListBlocks, ref h3, ref body);
        }
        else if (block.ContentTypeKey == EditorTabsPrimaryKey.Value ||
            block.ContentTypeKey == EditorAccordionPrimaryKey.Value)
        {
            DesiccateChildren(block, EditorPanel_PanelBlocks, ref h3, ref body);
        }
        else if (block.ContentTypeKey == EditorTextPrimaryKey.Value ||
            block.ContentTypeKey == EditorTextEmbeddedKey.Value ||
            block.ContentTypeKey == EditorTextPanelKey.Value)
        {
            var text = RichTextField(block, EditorTextEmbedded_Text);
            if (!string.IsNullOrWhiteSpace(text))
            {
                body.Add(text);
            }
            var textFromBlocks = RichTextBlocks(block, EditorTextEmbedded_Text);
            if (!string.IsNullOrWhiteSpace(textFromBlocks))
            {
                body.Add(textFromBlocks);
            }
        }
        else if (block.ContentTypeKey == EditorNotePrimaryKey.Value ||
            block.ContentTypeKey == EditorNoteEmbeddedKey.Value ||
            block.ContentTypeKey == EditorNotePanelKey.Value)
        {
            var header = RichTextField(block, EditorNoteBase_Header);
            if (!string.IsNullOrWhiteSpace(header))
            {
                h3.Add(header);
                body.Add(header);
            }

            var text = RichTextField(block, EditorNoteBase_Text);
            if (!string.IsNullOrWhiteSpace(text))
            {
                body.Add(text);
            }
        }
        else if (block.ContentTypeKey == EditorStepperEmbeddedKey.Value ||
            block.ContentTypeKey == EditorStepperPanelKey.Value ||
            block.ContentTypeKey == EditorStepperPrimaryKey.Value)
        {
            var steps = Blocks(block.Value<string>(EditorStepperEmbedded_Steps));
            if (steps?.Any() == true)
            {
                foreach (var step in steps)
                {
                    var header = RichTextField(block, EditorStep_Header);
                    if (!string.IsNullOrWhiteSpace(header))
                    {
                        h3.Add(header);
                        body.Add(header);
                    }

                    var text = RichTextField(block, EditorStep_Text);
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        body.Add(text);
                    }
                }
            }
        }
        else if (block.ContentTypeKey == EditorOpeningTimesEmbeddedKey.Value ||
            block.ContentTypeKey == EditorOpeningTimesPrimaryKey.Value ||
            block.ContentTypeKey == EditorOpeningTimesPanelKey.Value)
        {
            var header = RichTextField(block, EditorOpeningTimesEmbedded_Header);
            if (!string.IsNullOrWhiteSpace(header))
            {
                h3.Add(header);
                body.Add(header);
            }

            var text = RichTextField(block, EditorOpeningTimesEmbedded_Text);
            if (!string.IsNullOrWhiteSpace(text))
            {
                body.Add(text);
            }

            var footer = RichTextField(block, EditorOpeningTimesEmbedded_Footer);
            if (!string.IsNullOrWhiteSpace(footer))
            {
                body.Add(footer);
            }
        }
        else if (block.ContentTypeKey == EditorVideoPrimaryKey.Value ||
            block.ContentTypeKey == EditorVideoEmbeddedKey.Value ||
            block.ContentTypeKey == EditorVideoPanelKey.Value)
        {
            var header = RichTextField(block, EditorVideoEmbedded_Header);
            if (!string.IsNullOrWhiteSpace(header))
            {
                h3.Add(header);
                body.Add(header);
            }

            var copyright = RichTextField(block, EditorVideoEmbedded_Copyright);
            if (!string.IsNullOrWhiteSpace(copyright))
            {
                body.Add(copyright);
            }
        }
    }

    public IDictionary<string, object> Desiccate(IContent content)
    {
        var results = new Dictionary<string, object>();
        var h2 = new List<string>();
        var h3 = new List<string>();
        var body = new List<string>();

        if (!content.Published)
        {
            return results;
        }

        var path = content.Path.Split(',').Skip(1);
        if (path?.Any() != true)
        {
            return results;
        }

        results.Add(DomainId, path.First());
        results.Add(ContentId, content.Id);
        results.Add(Path, string.Join(' ', path));

        var categories = content.GetValue<string>(EditorPage_Categories);
        if (!string.IsNullOrWhiteSpace(categories))
        {
            logger.LogInformation($"Found categories {categories} in {content.Name}");
            results.Add(Categories, categories.Replace("umb://document/", "").Replace(',', ' '));
        }
        var keywords = content.GetValue<string>(EditorPage_Keywords);
        results.Add(Keywords, !string.IsNullOrWhiteSpace(keywords) ? string.Join(' ', keywords.Split('\n')) : "");
        var promoted = content.GetValue<string>(EditorPage_Promoted);
        results.Add(Promoted, !string.IsNullOrWhiteSpace(promoted) ? string.Join(' ', promoted.Split('\n')) : "");
        results.Add(Priority, content.GetValue<int>(EditorPage_SeoPriority));
        var seoAbstract = RichTextField(content, EditorPage_Abstract);
        var fieldAbstract = seoAbstract.IsNullOrWhiteSpace()
            ? content.GetValue<string>(EditorPage_Description)
            : seoAbstract;
        if (!fieldAbstract.IsNullOrWhiteSpace())
        {
            results.Add(Abstract, fieldAbstract);
        }
        var pageHeader = RichTextField(content, EditorPage_PageHeader);
        var header = pageHeader.IsNullOrWhiteSpace()
            ? content.GetValue<string>(EditorPage_Title)
                ?? content.Name
                ?? content.Id.ToString()
            : pageHeader;
        results.Add(Headers[0], header);
        body.Add(header);

        var blocks = Blocks(content.GetValue<string>(EditorPage_Body));
        if (blocks?.Any() == true)
        {
            foreach (var block in blocks)
            {
                if (block.Value<bool>(EditorBlockPrimary_Enable) == false)
                {
                    continue;
                }

                var mainTitle = RichTextField(block, EditorBlockPrimary_MainTitle);
                if (!string.IsNullOrWhiteSpace(mainTitle))
                {
                    h2.Add(mainTitle);
                    body.Add(mainTitle.EnsureEndsWith("."));

                }

                Desiccate(block, ref h3, ref body);

            }
        }

        if (h2.Any())
        {
            results.Add(Headers[1], string.Join(' ', h2));
        }
        if (h3.Any())
        {
            results.Add(Headers[2], string.Join(' ', h3));
        }
        if (body.Any())
        {
            results.Add(Body, string.Join(' ', body));
        }
        return results;
    }

    public IEnumerable<string> ParseCriteria(string criteria)
    {
        var items = new List<string>();
        var current = new StringBuilder();
        var insideQuotes = false;
        var whitespace = false;

        foreach (var ch in criteria)
        {
            if (char.IsNumber(ch) || char.IsLetter(ch))
            {
                if (whitespace)
                {
                    current.Append(' ');
                }
                current.Append(ch);
                whitespace = false;
            }
            else if (!insideQuotes && (ch == '"' || ch == '\''))
            {
                insideQuotes = true;
                whitespace = false;
                current.Append('"');
            }
            else if (insideQuotes)
            {
                whitespace = true;
            }
            else if (!insideQuotes && current.Length != 0)
            {
                items.Add(current.ToString());
                current = new StringBuilder();
            }
        }

        if (insideQuotes)
        {
            current.Append('"');
        }
        if (current.Length > 1)
        {
            items.Add(current.ToString());
        }
        return items;
    }

    private string Priorites(int[] priorites)
    {
        var query = new StringBuilder();
        for (int priority = 10, index = 0; priority >= 0 && index < priorites.Length; priority--, index++)
        {
            if (index != 0)
            {
                query.Append(" OR ");
            }

            query.Append(Priority);
            query.Append(':');
            query.Append(priority);
            query.Append('^');
            query.Append(priorites[index]);
        }
        return query.ToString();
    }

    private class Fragment
    {
        public int Start { get; init; }
        public int End { get; init; }
        public required string Text { get; init; }
    }

    private static IEnumerable<Fragment> FindMatches(string value, IEnumerable<string> texts)
    {
        var matches = new List<Fragment>();

        foreach (var text in texts)
        {
            foreach (var word in value.WordsAndPositions())
            {
                if (string.Compare(text, word.Item1, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    matches.Add(new Fragment
                    {
                        Start = word.Item2,
                        End = word.Item2 + text.Length,
                        Text = text
                    });
                }
            }
        }

        return matches.OrderBy(m => m.Start);
    }

    private static Tuple<int, IEnumerable<string>> FuzzyTexts(string value, IEnumerable<string> texts)
    {
        var didYouMean = new List<string>();
        var lowest = int.MaxValue;
        foreach (var text in texts)
        {
            foreach (var word in value.Words())
            {
                var distance = text.FuzzyMatch(word);
                if (distance < lowest)
                {
                    didYouMean.Clear();
                    lowest = distance;
                    didYouMean.Add(word);
                }
                else if (distance == lowest)
                {
                    didYouMean.Add(word);
                }
            }
        }
        return new Tuple<int, IEnumerable<string>>(lowest, didYouMean);
    }


    private static string? HighlightMatches(string? value, int start, IEnumerable<Fragment> fragments, IEditorSearchOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (!fragments.Any())
        {
            return value;
        }

        var output = new StringBuilder();
        var tag = 0;
        foreach (var ch in value)
        {
            if (fragments.Any(f => f.End == start))
            {
                if (--tag == 0)
                {
                    output.Append(options.HighlightCloseTag);
                }
            }

            if (fragments.Any(f => f.Start == start))
            {
                if (tag++ == 0)
                {
                    output.Append(options.HighlightOpenTag);
                }
            }

            output.Append(ch);
            start++;
        }

        if (tag != 0)
        {
            output.Append(options.HighlightCloseTag);
        }

        return output.ToString();
    }

    private static Fragment ReframeFragments(string value, IEnumerable<Fragment> fragments, IEditorSearchOptions options)
    {
        var half = options.HighlightFragmentSizeInCharacters / 2;
        var start = 0;
        var end = value.Length - 1;
        if (fragments?.Any() == true)
        {
            if (fragments.First().Start > half)
            {
                if (fragments.Last().End + half >= value.Length)
                {
                    if (value.Length - options.HighlightFragmentSizeInCharacters > 0)
                    {
                        start = value.Length - options.HighlightFragmentSizeInCharacters;
                    }
                }
                else
                {
                    start = fragments.First().Start - half;
                    end = fragments.Last().End + half;
                }
            }
            else if (fragments.Last().End + half < value.Length)
            {
                end = fragments.Last().End + half;
            }
        }

        while (start > 0 && !char.IsWhiteSpace(value[start]))
        {
            start--;
        }

        while (end < value.Length - 1 && !char.IsWhiteSpace(value[end]))
        {
            end++;
        }
        return new Fragment
        {
            Start = start,
            End = end,
            Text = value.Substring(start, end - start + 1)
        };
    }

    private IEnumerable<IEnumerable<Fragment>> GroupFragments(IEnumerable<Fragment> fragments, IEditorSearchOptions options)
    {
        var merged = new List<IEnumerable<Fragment>>();
        var current = new List<Fragment>();
        foreach (var fragment in fragments)
        {
            if (current.Any())
            {
                var last = current.Last();
                if (last.End < fragment.Start - options.HighlightFRagmentsMergeInCharacters)
                {
                    merged.Add(current);
                    current = new List<Fragment>();
                }
            }
            current.Add(fragment);
        }
        merged.Add(current);

        return merged;
    }

    private string? Highlight(string? value, IEnumerable<string> texts, IEditorSearchOptions options) => 
        string.IsNullOrWhiteSpace(value) ? null : HighlightMatches(value, 0, FindMatches(value, texts), options);

    private string? HighlightInGroups(string? value, IEnumerable<string> texts, IEditorSearchOptions options)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var matches = FindMatches(value, texts);
        var groups = GroupFragments(matches, options);
        var body = new StringBuilder();

        foreach (var group in groups.Take(options.HighlightMaximumNumberOfFragments))
        {
            if (body.Length != 0)
            {
                body.Append(options.HighlightFragmentSeparator);
            }

            var reframe = ReframeFragments(value, group, options);
            body.Append(HighlightMatches(reframe.Text, reframe.Start, group, options));
        }

        return body.ToString();
    }

    private EditorSearchResult Hydrate(ISearchResult result, IEnumerable<string> texts, bool fuzzy, IEditorSearchOptions options)
    {
        return new EditorSearchResult
        {
            ContentId = int.Parse(result.Id),
            DomainId = int.Parse(result.Values[DomainId]),
            Path = result.Values[Path].Split(' ').Select(p => int.Parse(p)).ToArray(),
            Priority = int.Parse(result.Values[Priority]),
            Headers = Headers.Select(h => Highlight(result.Values[h], texts, options)).ToArray(),
            Body = HighlightInGroups(result.Values[Body], texts, options),
            Categories = new Guid[0],
            Abstract = result.Values[Abstract]
        };
    }

    public IEditorSearchResults SearchExact(int domainId, int[] boosts, int[] priorites, IEnumerable<string> texts, int page, int pageSize, IEditorSearchOptions options)
    {
        if (!examineManager.TryGetIndex(nameof(EditorIndex), out var index))
        {
            throw new ApplicationException("Unable to find index " + nameof(EditorIndex));
        }

        var query = new StringBuilder();

        query.Append(DomainId);
        query.Append(':');
        query.Append(domainId);
        query.Append(" AND (");

        foreach (var header in TextFields.Select((Text,Index) => (Index,Text)))
        {
            if (header.Index != 0)
            {
                query.Append(" OR ");
            }

            if (texts.Count() > 1)
            {
                query.Append('(');
            }
            foreach (var text in texts.Select((Text, Index) => (Index, Text)))
            {
                if (text.Index != 0) 
                {
                    query.Append(" AND ");
                }

                query.Append(header.Text);
                query.Append(':');
                query.Append(text.Text);
                if (boosts.Length > header.Index && boosts[header.Index] > 0)
                {
                    query.Append('^');
                    query.Append(boosts[header.Index]);
                }
            }
            if (texts.Count() > 1)
            {
                query.Append(')');
            }
        }

        query.Append(')');
        if (priorites.Any())
        {
            query.Append(" AND (");
            query.Append(Priorites(priorites));
            query.Append(')');
        }
        var results = index.Searcher.CreateQuery().NativeQuery(query.ToString()).Execute(new Examine.Search.QueryOptions(page * pageSize, pageSize));

        return new EditorSearchResults
        {
            DidYouMean = null,
            Results = results.Select(r => Hydrate(r, string.Join(' ', texts).Words(), false, options)),
            Page = page,
            PageSize = pageSize,
            TotalResults = (int) results.TotalItemCount
        };
    }

    public IEditorSearchResults SearchFuzzy(int domainId, double fuzzy, int[] priorites, IEnumerable<string> texts, int page, int pageSize, IEditorSearchOptions options)
    {
        if (!examineManager.TryGetIndex(nameof(EditorIndex), out var index))
        {
            throw new ApplicationException("Unable to find index " + nameof(EditorIndex));
        }

        var query = new StringBuilder();

        query.Append(DomainId);
        query.Append(':');
        query.Append(domainId);
        query.Append(" AND (");

        foreach (var header in TextFields.Select((Text, Index) => (Index, Text)))
        {
            if (header.Index != 0)
            {
                query.Append(" OR ");
            }

            if (texts.Count() > 1)
            {
                query.Append('(');
            }
            foreach (var text in texts.Select((Text, Index) => (Index, Text)))
            {
                if (text.Index != 0)
                {
                    query.Append(" AND ");
                }

                query.Append(header.Text);
                query.Append(':');
                query.Append(text.Text);
                query.Append('~');
                query.Append(fuzzy.ToString("0.00"));
            }
            if (texts.Count() > 1)
            {
                query.Append(')');
            }
        }

        query.Append(')');
        if (priorites.Any())
        {
            query.Append(" AND (");
            query.Append(Priorites(priorites));
            query.Append(')');
        }

        var results = index.Searcher.CreateQuery().NativeQuery(query.ToString()).Execute(new Examine.Search.QueryOptions(page * pageSize, pageSize));

        var didYouMean = new List<string>();
        var criteria = string.Join(' ', texts).Words();
        foreach (var result in results)
        {
            foreach (var fuzzyText in FuzzyTexts(result.Values[Body], criteria).Item2)
            {
                if (!didYouMean.Contains(fuzzyText) && !texts.Contains(fuzzyText))
                {
                    didYouMean.Add(fuzzyText);
                }
            }
        }

        return new EditorSearchResults
        {
            DidYouMean = didYouMean.Order(),
            Results = results.Select(r => Hydrate(r, didYouMean, true, options)),
            Page = page,
            PageSize = pageSize,
            TotalResults = (int)results.TotalItemCount
        };
    }

    public IEnumerable<IEditorSearchResult> SearchCategories(int domainId, IEnumerable<Guid> categories, IEditorSearchOptions options)
    {
        if (!examineManager.TryGetIndex(nameof(EditorIndex), out var index))
        {
            throw new ApplicationException("Unable to find index " + nameof(EditorIndex));
        }

        var query = new StringBuilder();

        query.Append(DomainId);
        query.Append(':');
        query.Append(domainId);
        query.Append(" AND (");
        foreach (var category in categories.Select((Category, Index) => (Category, Index)))
        {
            if (category.Index != 0)
            {
                query.Append(" OR ");
            }

            query.Append(Categories);
            query.Append(':');
            query.Append(category.Category.ToString("n"));
        }
        query.Append(')');

        var results = index.Searcher.CreateQuery().NativeQuery(query.ToString()).Execute();

        return results.Select(r => Hydrate(r, new string[0], true, options));
    }
}
