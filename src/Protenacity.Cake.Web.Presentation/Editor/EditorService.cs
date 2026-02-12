using Microsoft.Extensions.Logging;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Accordion;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.Editor.Card;
using Protenacity.Cake.Web.Presentation.Editor.Category;
using Protenacity.Cake.Web.Presentation.Editor.ExpandableSection;
using Protenacity.Cake.Web.Presentation.Editor.Form;
using Protenacity.Cake.Web.Presentation.Editor.FormEntry;
using Protenacity.Cake.Web.Presentation.Editor.Group;
using Protenacity.Cake.Web.Presentation.Editor.Image;
using Protenacity.Cake.Web.Presentation.Editor.List;
using Protenacity.Cake.Web.Presentation.Editor.Map;
using Protenacity.Cake.Web.Presentation.Editor.OpeningTimes;
using Protenacity.Cake.Web.Presentation.Editor.Paging;
using Protenacity.Cake.Web.Presentation.Editor.Script;
using Protenacity.Cake.Web.Presentation.Editor.Search;
using Protenacity.Cake.Web.Presentation.Editor.SectionLinks;
using Protenacity.Cake.Web.Presentation.Editor.Stepper;
using Protenacity.Cake.Web.Presentation.Editor.Table;
using Protenacity.Cake.Web.Presentation.Editor.Tabs;
using Protenacity.Cake.Web.Presentation.Editor.Text;
using Protenacity.Cake.Web.Presentation.Editor.Video;
using Protenacity.Cake.Web.Presentation.Form;
using Protenacity.Cake.Web.Presentation.View;
using Sagara.FeedReader;
using Sagara.FeedReader.Feeds;
using System.Collections.ObjectModel;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Editor;

internal class EditorService(
    ILogger<EditorService> logger,
    IViewService viewService, 
    IUmbracoHelperAccessor umbracoHelperAccessor, 
    IFusionCache fusionCache,
    FeedReader feedReaderService) : IEditorService
{
    public BlockListItem<TContent, TSettings> Cast<TContent, TSettings>(BlockListItem block)
        where TContent : IPublishedElement
        where TSettings : IPublishedElement
    {
#if NET9_0_OR_GREATER
        return new BlockListItem<TContent, TSettings>(block.ContentKey, (TContent)((IPublishedElement)block.Content), block.SettingsKey, (TSettings?)((IPublishedElement?)block.Settings));
#else
        return new BlockListItem<TContent, TSettings>(block.ContentUdi, (TContent)((IPublishedElement)block.Content), block.SettingsUdi, (TSettings)((IPublishedElement)block.Settings));
#endif
    }

    public IEditorContentPaging? Load(Guid? parentContentKey, EditorDefaults? defaults, IEnumerable<BlockListItem>? sources) =>
        sources == null ? null : Load(parentContentKey, defaults, new ReadOnlyCollection<BlockListItem>(sources.ToList()));

    public IEditorContentPaging? Load(Guid? parentContentKey, EditorDefaults? defaults, ReadOnlyCollection<BlockListItem>? sources)
    {
        var blocks = new List<IEditorContent>();
        IPagingViewModel? paging = null;

        if (sources?.Any() != true)
        {
            return null;
        }

        foreach (var source in sources)
        {
            var primary = source.Content as IEditorBlockPrimary;

            if (primary?.Enable == false)
            {
                continue;
            }

            var index = blocks.Count();
            var content = new EditorContent
            {
                Index = index,
                Id = EncodeId(string.IsNullOrWhiteSpace(primary?.Identifier) ? "block-" + index.ToString() : primary.Identifier.Trim()),
                Header = primary?.MainTitle?.HasContent() == true 
                    ? primary?.MainTitle 
                    : (source.Content as IEditorPanelName)?.PanelName?.HasContent() == true 
                        ? (source.Content as IEditorPanelName)?.PanelName 
                        : new HtmlEncodedString(primary?.Identifier ?? index.ToString()),
                EditorComponent = string.Empty,
                Block = source,
                Defaults = defaults ?? new EditorDefaults()
            };

            switch (source.Content.ContentType.Alias)
            {
                case EditorSelectNodes.ModelTypeAlias:
                    blocks.AddRange(LoadContent(index, content.Defaults, source));
                    continue;   // Next foreach

                case EditorSelectNodesAside.ModelTypeAlias:
                    content.Defaults.CardStyleActionAlignment = ActionStyleAlignments.LeftRelative;
                    blocks.AddRange(LoadContent(index, content.Defaults, source));
                    continue;   // Next foreach

                case EditorChildNodes.ModelTypeAlias:
                    blocks.AddRange(LoadChildContent(index, content.Defaults, parentContentKey, source));
                    continue;   // Next foreach

                case EditorChildNodesAside.ModelTypeAlias:
                    content.Defaults.CardStyleActionAlignment = ActionStyleAlignments.LeftRelative;
                    blocks.AddRange(LoadChildContent(index, content.Defaults, parentContentKey, source));
                    continue;   // Next foreach

                case EditorSelectMedia.ModelTypeAlias:
                    blocks.AddRange(LoadMedia(index, MediaViewComponent.Name, content.Defaults, source));
                    continue;   // Next foreach

                case EditorSelectMediaAside.ModelTypeAlias:
                    content.Defaults.CardStyleActionAlignment = ActionStyleAlignments.LeftRelative;
                    blocks.AddRange(LoadMedia(index, MediaViewComponent.Name, content.Defaults, source));
                    continue;   // Next foreach

                //case EditorFormEntriesEmbedded.ModelTypeAlias:
                //    var contentPaging = LoadFormEntries(index, content.Defaults, source);
                //    if (contentPaging?.Item2.Any() == true)
                //    {
                //        paging = contentPaging.Item1;
                //        blocks.AddRange(contentPaging.Item2);
                //    }
                //    continue;

                case EditorRssFeed.ModelTypeAlias:
                    blocks.AddRange(LoadRssFeed(index, content.Defaults, source));
                    continue;

                case EditorCategoriesPrimary.ModelTypeAlias:
                case EditorCategoriesEmbedded.ModelTypeAlias:
                    content.EditorComponent = CategoryViewComponent.Name;
                    break;

                case EditorTabsPrimary.ModelTypeAlias:
                    content.EditorComponent = TabsViewComponent.Name;
                    break;

                case EditorAccordionPrimary.ModelTypeAlias:
                    content.EditorComponent = AccordionViewComponent.Name;
                    break;

                case EditorCardPrimary.ModelTypeAlias:
                case EditorCardEmbedded.ModelTypeAlias:
                case EditorCardPanel.ModelTypeAlias:
                    content.EditorComponent = CardViewComponent.Name;
                    break;

                case EditorTextPrimary.ModelTypeAlias:
                case EditorTextEmbedded.ModelTypeAlias:
                case EditorTextPanel.ModelTypeAlias:
                    content.EditorComponent = TextViewComponent.Name;
                    break;

                case EditorImageEmbedded.ModelTypeAlias:
                case EditorImagePrimary.ModelTypeAlias:
                case EditorImagePanel.ModelTypeAlias:
                    content.EditorComponent = ImageViewComponent.Name;
                    break;

                case EditorListPrimary.ModelTypeAlias:
                case EditorListEmbedded.ModelTypeAlias:
                case EditorListPanel.ModelTypeAlias:
                    content.EditorComponent = ListViewComponent.Name;
                    break;

                case EditorTablePrimary.ModelTypeAlias:
                case EditorTablePanel.ModelTypeAlias:
                case EditorTableEmbedded.ModelTypeAlias:
                    content.EditorComponent = TableViewComponent.Name;
                    break;

                case EditorVideoPrimary.ModelTypeAlias:
                case EditorVideoEmbedded.ModelTypeAlias:
                case EditorVideoPanel.ModelTypeAlias:
                    content.EditorComponent = VideoViewComponent.Name;
                    break;

                case EditorScriptPrimary.ModelTypeAlias:
                case EditorScriptPanel.ModelTypeAlias:
                case EditorScriptEmbedded.ModelTypeAlias:
                    content.EditorComponent = ScriptViewComponent.Name;
                    break;

                case EditorActionEmbedded.ModelTypeAlias:
                case EditorActionAside.ModelTypeAlias:
                    content.EditorComponent = ActionViewComponent.Name;
                    break;

                case EditorSectionLinksEmedded.ModelTypeAlias:
                case EditorSectionLinksPrimary.ModelTypeAlias:
                    content.EditorComponent = SectionLinksViewComponent.Name;
                    break;

                case EditorOpeningTimesEmbedded.ModelTypeAlias:
                case EditorOpeningTimesPrimary.ModelTypeAlias:
                case EditorOpeningTimesPanel.ModelTypeAlias:
                    content.EditorComponent = OpeningTimesViewComponent.Name;
                    break;

                case EditorStepperEmbedded.ModelTypeAlias:
                case EditorStepperPanel.ModelTypeAlias:
                case EditorStepperPrimary.ModelTypeAlias:
                    content.EditorComponent = StepperViewComponent.Name;
                    break;

                case EditorFormEmbedded.ModelTypeAlias:
                case EditorFormPrimary.ModelTypeAlias:
                case EditorFormPanel.ModelTypeAlias:
                    content.EditorComponent = FormViewComponent.Name;
                    break;

                case EditorMapEmbedded.ModelTypeAlias:
                case EditorMapPrimary.ModelTypeAlias:
                case EditorMapPanel.ModelTypeAlias:
                    content.EditorComponent = MapViewComponent.Name;
                    break;

                case EditorGroupAside.ModelTypeAlias:
                    content.EditorComponent = GroupViewComponent.Name;
                    break;

                case EditorSearchPrimary.ModelTypeAlias:
                    content.EditorComponent = SearchViewComponent.Name;
                    break;

                case EditorExpandableSectionPrimary.ModelTypeAlias:
                    content.EditorComponent = ExpandableSectionViewComponent.Name;
                    break;

                case EditorSelectMediaPrimary.ModelTypeAlias:
                    blocks.AddRange(LoadMedia(index, DownloadMediaViewComponent.Name, content.Defaults, source));
                    continue;   // Next foreach

                default:
                    throw new ApplicationException(source.Content.ContentType.Alias + " is unknown Editor Component");
            }
            blocks.Add(content);
        }

        return new EditorContentPaging
        {
            Contents = blocks,
            Paging = paging
        };
    }

    private IEnumerable<EditorPage> OrderPages(IEnumerable<EditorPage> pages, EditorOrders order)
    {
        switch (order)
        {
            case EditorOrders.Oldest:
                return pages.OrderBy(p => p.CreateDate);

            case EditorOrders.Latest:
                return pages.OrderByDescending(p => p.CreateDate);

            case EditorOrders.AtoZ:
                return pages.OrderBy(p => p.PageTitleName?.HasContent() == true ? p.PageTitleName.ToText().ToString() : p.Name);

            case EditorOrders.ZtoA:
                return pages.OrderByDescending(p => p.PageTitleName?.HasContent() == true ? p.PageTitleName.ToText().ToString() : p.Name);

            default:
                return pages;
        }
    }

    private IEnumerable<IPublishedContent> OrderMedia(IEnumerable<IPublishedContent> items, EditorOrders order)
    {
        switch (order)
        {
            case EditorOrders.Oldest:
                return items.OrderBy(p => p.CreateDate);

            case EditorOrders.Latest:
                return items.OrderByDescending(p => p.CreateDate);

            case EditorOrders.AtoZ:
                return items.OrderBy(p => p.Name);

            case EditorOrders.ZtoA:
                return items.OrderByDescending(p => p.Name);

            default:
                return items;
        }
    }

    private string EncodeId(string text) => System.Web.HttpUtility.HtmlEncode(text.Replace(' ', '-').ToLowerInvariant());

    private EditorDefaults SetDefault(EditorDefaults? defaults, IPublishedElement? settings)
    {
        if (defaults == null)
        {
            defaults = new EditorDefaults();
        }

        if (settings != null)
        {
            var cardSettings = settings as IEditorCardBaseSettings;
            var actionSettings = settings as IEditorActionEmbeddedSettings;
            var backgroundSettings = settings as IEditorBackgroundSettings;
            var borderSettings = settings as IEditorBorderSettings;

            if (cardSettings?.StyleImage != null)
            {
                defaults.CardStyleImageLocation = cardSettings.StyleImage;
            }

            if (cardSettings?.StyleHeader != null)
            {
                defaults.CardStyleHeader = cardSettings.StyleHeader;
            }

            if (cardSettings?.StyleDate != null)
            {
                defaults.CardStyleDate = cardSettings.StyleDate;
            }

            if (cardSettings?.StyleTime != null)
            {
                defaults.CardStyleTime = cardSettings.StyleTime;
            }

            if (cardSettings?.StyleText != null)
            {
                defaults.CardStyleText = cardSettings.StyleText;
            }

            if (actionSettings?.StyleAction != null)
            {
                defaults.CardStyleAction = actionSettings.StyleAction;
            }

            if (backgroundSettings?.OverrideColor?.Any() == true)
            {
                defaults.CardStyleOverrideColor = backgroundSettings?.OverrideColor;
            }

            if (!string.IsNullOrWhiteSpace(borderSettings?.BorderColor?.Color))
            {
                defaults.CardStyleBorderColor = borderSettings?.BorderColor.Color;
            }
        }
        return defaults;
    }

    private IEnumerable<IEditorContent> LoadContent(int index, EditorDefaults? defaults, BlockListItem source)
    {
        var blocks = new List<IEditorContent>();
        var content = source.Content as IEditorSelectNodesBase;
        var orderSettings = source.Settings as IEditorOrderSettings;

        if (content?.Pages?.Any() != true)
        {
            return blocks;
        }

        defaults = SetDefault(defaults, source.Settings);
        var pages = content.Pages.Where(c => c.IsVisible() && c.ContentType.Alias == EditorPage.ModelTypeAlias).Cast<EditorPage>();

        foreach (var page in OrderPages(pages, orderSettings?.OrderTyped ?? EditorOrders.Default))
        {
            var header = string.IsNullOrWhiteSpace(page.Title) ? page.Name : page.Title;
            blocks.Add(new EditorContent
            {
                Index = index++,
                Id = EncodeId("page-" + header),
                Header = page.PageTitleName?.HasContent() == true ? page.PageTitleName : new HtmlEncodedString(header),
                EditorComponent = PageViewComponent.Name,
                Block = new BlockListItem(page.Key, page, source.SettingsKey, source.Settings),
                Defaults = defaults
            });
        }

        return blocks;
    }

    private IEnumerable<IEditorContent> LoadChildContent(int index, EditorDefaults? defaults, Guid? parentContentKey, BlockListItem source)
    {
        var blocks = new List<IEditorContent>();
        var orderSettings = source.Settings as IEditorOrderSettings;
        var parent = (parentContentKey != null ? (umbracoHelperAccessor.TryGetUmbracoHelper(out var umbracoHelper) ? umbracoHelper.Content(parentContentKey) : null) : null) ?? viewService.CurrentPage;
        defaults = SetDefault(defaults, source.Settings);

        var status = (parent as EditorPage)?.SeoStatusTyped;
        var ancestor = parent;
        while (status == SeoStatuses.Inherit && ancestor != null)
        {
            ancestor = ancestor.Parent();
            status = (ancestor as EditorPage)?.SeoStatusTyped;
        }

        if (status == SeoStatuses.Inherit)
        {
            //  Home page has invalid status
            logger.LogWarning($"Home page of {parent.Name} ({parent.Id}) has invalid Seo Status");
            status = SeoStatuses.Disable;
        }

        var children = parent.ChildrenOfType(EditorPage.ModelTypeAlias)?
            .Where(c => c.IsVisible() && c.ContentType.Alias == EditorPage.ModelTypeAlias)
            .Cast<EditorPage>()
            .Where(c => c.SeoStatusTyped == SeoStatuses.Enable || (c.SeoStatusTyped == SeoStatuses.Inherit && status == SeoStatuses.Enable));
        if (children?.Any() == true)
        {
            foreach (var page in OrderPages(children, orderSettings?.OrderTyped ?? EditorOrders.Default))
            {
                var header = string.IsNullOrWhiteSpace(page.Title) ? page.Name : page.Title;
                blocks.Add(new EditorContent
                {
                    Index = index++,
                    Id = EncodeId("page-" + header),
                    Header = page.PageTitleName?.HasContent() == true ? page.PageTitleName : new HtmlEncodedString(header),
                    EditorComponent = PageViewComponent.Name,
                    Block = new BlockListItem(page.Key, page, source.SettingsKey, source.Settings),
                    Defaults = defaults
                });
            }
        }
        return blocks;
    }

    private IEnumerable<IPublishedContent> GetMedia(IEnumerable<IPublishedContent> sources)
    {
        var medias = new List<IPublishedContent>();

        foreach (var source in sources)
        {
            switch (source.ContentType.Alias)
            {
                case Core.Constitution.Folder.ModelTypeAlias:

                    var children = source.ChildrenOfType(Core.Constitution.EditorDownload.ModelTypeAlias);
                    if (children?.Any() == true)
                    {
                        medias.AddRange(GetMedia(children));
                    }
                    break;

                case Core.Constitution.EditorDownload.ModelTypeAlias:
                    medias.Add(source);
                    break;
            }
        }
        return medias;
    }

    private IEnumerable<IEditorContent> LoadMedia(int index, string editorComponent, EditorDefaults? defaults, BlockListItem source)
    {
        var content = source.Content as IEditorSelectMedia;
        var orderSettings = source.Settings as IEditorOrderSettings;
        var blocks = new List<IEditorContent>();

        if (content?.MediaItems?.Any() != true)
        {
            return blocks;
        }
        if (defaults == null)
        {
            defaults = new EditorDefaults();
        }
        defaults.CardStyleImageLocation = EditorCardStyleImageLocations.Hide;
        defaults = SetDefault(defaults, source.Settings);

        foreach (var item in OrderMedia(GetMedia(content.MediaItems), orderSettings?.OrderTyped ?? EditorOrders.Default))
        {
            blocks.Add(new EditorContent
            {
                Index = index++,
                Id = EncodeId("media-" + item.Name),
                Header = new HtmlEncodedString(item.Name),
                EditorComponent = editorComponent,
#if NET9_0_OR_GREATER
                Block = new BlockListItem(item.Key, item, source.SettingsKey, source.Settings),
#else
                Block = new BlockListItem(Udi.Create(Constants.UdiEntityType.Document, item.Key), item, source.SettingsUdi, source.Settings),
#endif
                Defaults = defaults
            });
        }

        return blocks;
    }

    //private Tuple<IPagingViewModel?, IEnumerable<IEditorContent>>? LoadFormEntries(int index, EditorDefaults? defaults, BlockListItem source)
    //{
    //    var blocks = new List<IEditorContent>();
    //    var content = source.Content as EditorFormEntriesEmbedded;
    //    defaults = SetDefault(defaults, source.Settings);

    //    if (content?.Source == null)
    //    {
    //        return null;
    //    }

    //    int page = viewService.CurrentSearchPage ?? 0;
    //    int pagesize = viewService.CurrentSearchPageSize ?? content.Maximum;

    //    var entries = formReaderService.ApprovedEntriesByDate((Guid)content.Source,
    //        viewService.CurrentDomainPage?.ConfigUmbracoFormFieldName ?? "name",
    //        viewService.CurrentDomainPage?.ConfigUmbracoFormFieldNameDefault ?? "message",
    //        viewService.CurrentDomainPage?.ConfigUmbracoFormFieldMessage ?? "Anonymous",
    //        page,
    //        pagesize,
    //        viewService.CurrentDomainPage?.ConfigUmbracoFormCacheLength != null ? new TimeSpan((TimeSpan.TicksPerMinute * ((long)viewService.CurrentDomainPage.ConfigUmbracoFormCacheLength)) + 1L) : null);
    //    ;
    //    if (entries?.Entries.Any() != true)
    //    {
    //        return null;
    //    }

    //    foreach (var entry in entries.Entries)
    //    {
    //        blocks.Add(new EditorContent<FormReaderEntry>
    //        {
    //            Index = index++,
    //            Id = EncodeId("formentry-" + entry.Name),
    //            Header = new HtmlEncodedString(entry.Name),
    //            EditorComponent = FormEntryViewComponent.Name,
    //            Block = source,
    //            Defaults = defaults,
    //            ExtraData = entry
    //        });
    //    }

    //    return new Tuple<IPagingViewModel?, IEnumerable<IEditorContent>>(entries.TotalEntries > entries.Entries.Count() ? new PagingViewModel
    //    {
    //        Page = page,
    //        PageSize = pagesize,
    //        QueryString = source.ContentKey.ToString("N"),
    //        TotalResults = entries.TotalEntries,
    //        Id = source.ContentKey.ToString("N")
    //    } : null, blocks);
    //}

    private IEnumerable<IEditorContent> LoadRssFeed(int index, EditorDefaults? defaults, BlockListItem source)
    {
        var blocks = new List<IEditorContent>();
        var content = source.Content as EditorRssFeed;
        var endpoint = (content?.Feed as RssFeedData)?.Endpoint;
        defaults = SetDefault(defaults, source.Settings);
        var cacheLength = TimeSpan.FromMinutes(viewService.CurrentDomainPage.ConfigRssFeedReaderCache);

        if (content == null || endpoint == null)
        {
            return blocks;
        }

        var feed = fusionCache.GetOrSet<Feed?>(nameof(Feed) + endpoint, (ctx, ct) =>
        {
            var feed = feedReaderService.ReadFromUrlAsync(endpoint, null, ct).GetAwaiter().GetResult();
            if (feed == null)
            {
                ctx.Options.Duration = cacheLength;
                try
                {
                    return ctx.NotModified();
                }
                catch
                {
                    return null;
                }
            }

            var ttl = (feed.SpecificFeed as Rss20Feed)?.TTL
                ?? (feed.SpecificFeed as MediaRssFeed)?.TTL;

            if (ttl != null && int.TryParse(ttl, out var mins))
            {
                ctx.Options.Duration = TimeSpan.FromMinutes(mins);
            }
            return feed;
        }, options => options.SetDuration(cacheLength));

        if (feed?.Items.Any() != true)
        {
            return blocks;
        }

        if (feed.Items?.Any() == true)
        {
            foreach (var entry in feed.Items.Take(content.Maximum > 0 ? content.Maximum : 1024))
            {
                blocks.Add(new EditorContent<EditorFeedItem>
                {
                    Index = index++,
                    Id = EncodeId("feed-" + entry.Id),
                    Header = new HtmlEncodedString(entry.Title ?? (string.IsNullOrEmpty(feed.Title) ? "" : feed.Title + " ") + index.ToString()),
                    EditorComponent = RssFeedViewComponent.Name,
                    Block = source,
                    Defaults = defaults,
                    ExtraData = new EditorFeedItem
                    {
                        Title = entry.Title,
                        Content = entry.Content,
                        Link = entry.Link,
                        Description = entry.Description,
                        PublishingDate = entry.PublishingDate,
                        Author = entry.Author,
                        Id = entry.Id,
                        SpecificItem = entry.SpecificItem,
                        ImageUrl = feed.ImageUrl
                    }
                });
            }
        }
        return blocks;
    }

}
