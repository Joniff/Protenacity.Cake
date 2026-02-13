using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.Search;
using Protenacity.Cake.Web.Presentation.View;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using ZiggyCreatures.Caching.Fusion;

namespace Protenacity.Cake.Web.Presentation.Category;

internal class CategoryService(IDocumentNavigationQueryService documentNavigationQueryService,
    IUmbracoContextFactory umbracoContextFactory,
    IEditorSearchService editorSearchService,
    IViewService viewService,
    IFusionCache fusionCache) : ICategoryService
{
    private IEnumerable<CategoryHeaderData> GetHeadings(IEnumerable<IPublishedContent> sources)
    {
        var headings = new List<CategoryHeaderData>();

        foreach (var source in sources)
        {
            switch (source.ContentType.Alias)
            {
                case CategoryHeaderData.ModelTypeAlias:
                    headings.Add(source as CategoryHeaderData ?? throw new InvalidDataException());
                    break;

                case CategorysData.ModelTypeAlias:
                    if (documentNavigationQueryService.TryGetChildrenKeysOfType(source.Key, CategoryHeaderData.ModelTypeAlias, out var childrenKeys))
                    {
                        using (var umbracoContext = umbracoContextFactory.EnsureUmbracoContext())
                        {
                            foreach (var childKey in childrenKeys)
                            {
                                var child = umbracoContext.UmbracoContext.Content.GetById(childKey) as CategoryHeaderData ?? throw new InvalidDataException();
                                headings.Add(child);
                            }
                        }
                    }
                    break;
            }
        }
        return headings;
    }

    private string? Description(CategoryHeaderData categoryHeaderData)
    {
        IHtmlEncodedString? text = null;

        switch (categoryHeaderData.HeadingDescriptionStatus)
        {
            case Core.Property.CategoryHeadingDescriptionStatuses.Inherit:
                var parent = categoryHeaderData.Parent() as CategorysData;
                if (parent != null)
                {
                    switch (parent.HeadingDescriptionStatus)
                    {
                        case Core.Property.CategoryHeadingDescriptionStatuses.Show:
                        case Core.Property.CategoryHeadingDescriptionStatuses.Inherit:
                            text = parent.HeadingDescription;
                            break;
                    }
                }
                break;

            case Core.Property.CategoryHeadingDescriptionStatuses.Show:
                text = categoryHeaderData.HeadingDescription;
                break;
        }

        if (text == null)
        {
            return null;
        }

        return viewService.Parse(text, new Dictionary<string, string>
        {
            { RichTextFields.CategoryHeading, categoryHeaderData.Name }
        });
    }

    public IEnumerable<CategoryHeading> GetCategories(DomainPage domainPage, IEnumerable<IPublishedContent> sources)
    {
        return fusionCache.GetOrSet<IEnumerable<CategoryHeading>>(nameof(GetCategories) + ":" + domainPage.Id.ToString() + ":" + string.Join(':', sources.Select(x => x.Key)), (ctx, ct) =>
        {
            var results = new List<CategoryHeading>();
            var headings = GetHeadings(sources);

            if (headings?.Any() != true)
            {
                return Enumerable.Empty<CategoryHeading>();
            }

            using (var umbracoContext = umbracoContextFactory.EnsureUmbracoContext())
            {
                foreach (var heading in headings)
                {
                    var names = new List<CategoryName>();

                    if (documentNavigationQueryService.TryGetChildrenKeysOfType(heading.Key, CategoryData.ModelTypeAlias, out var categoryKeys))
                    {
                        foreach (var key in categoryKeys)
                        {
                            var matches = editorSearchService.SearchCategoryHeading(domainPage, key);
                            if (matches?.Any() == true)
                            {
                                var pages = new List<Tuple<string, string>>();
                                foreach (var match in matches)
                                {
                                    var page = umbracoContext.UmbracoContext.Content.GetById(match.ContentId);
                                    if (page != null)
                                    {
                                        pages.Add(new Tuple<string, string>(match.Headers[0] ?? page.Name, page.Url()));
                                    }
                                }
                                if (pages.Any())
                                {
                                    var name = umbracoContext.UmbracoContext.Content.GetById(key);
                                    if (name != null)
                                    {
                                        names.Add(new CategoryName
                                        {
                                            Name = name.Name,
                                            PageNameUrls = pages
                                        });
                                    }
                                }
                            }
                        }

                        if (names.Any())
                        {
                            results.Add(new CategoryHeading
                            {
                                Title = heading.Name,
                                Description = Description(heading),
                                Names = names
                            });
                        }
                    }
                }
            }

            return results;
        }, options => options.SetDuration(TimeSpan.FromMinutes(domainPage.ConfigCategoryCache)));
    }
}

