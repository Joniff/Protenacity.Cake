using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor;
using Protenacity.Cake.Web.Presentation.Editor.Group;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Pages.Editor;

public class EditorPageController(
    IViewService viewService,
    IEditorService editorService,
    IPublishedValueFallback publishedValueFallback,
    ILogger<EditorPageController> logger,
    ICompositeViewEngine compositeViewEngine,
    IUmbracoContextAccessor umbracoContextAccessor) 
    : RenderController(logger, compositeViewEngine, umbracoContextAccessor)
{
    private IEnumerable<IEditorContents>? AsideToGroups(IAsideTab? aside, bool isMobile)
    {
        if (aside?.AsideContent?.Any() != true)
        {
            return null;
        }

        var groups = new List<IEditorContents>();
        List<BlockListItem>? items = null;

        void AddItemsToGroup()
        {
            if (items?.Any() == true)
            {
                groups.Add(new EditorContents
                {
                    Index = groups.Count,
                    Id = GroupViewComponent.Name + Guid.NewGuid().ToString("N"),
                    Gap = isMobile == true ? groups.Any() : false,
                    Header = null,
                    ParentKey = aside.Key,
                    EditorComponent = GroupViewComponent.Name,
                    Defaults = new EditorDefaults
                    {
                        CardStyleImageLocation = EditorCardStyleImageLocations.Hide,
                        CardStyleHeader = EditorCardStyleHeaders.Hide,
                        CardStyleDate = EditorCardStyleDates.Hide,
                        CardStyleTime = EditorCardStyleTimes.Hide,
                        CardStyleText = EditorCardStyleTexts.Hide,
                        CardStyleAction = ActionStyles.Link,
                        CardStyleActionClickArea = ActionStyleClickAreas.Action,
                        CardStyleActionAlignment = ActionStyleAlignments.RightAbsolute
                    },
                    Blocks = items
                });
            }
            items = null;
        }

        foreach (var block in aside.AsideContent)
        {
            if (block.Content.ContentType.Alias == EditorGroupAside.ModelTypeAlias)
            {
                AddItemsToGroup();
                var groupBlock = editorService.Cast<EditorGroupAside, EditorGroupAsideSettings>(block);

                groups.Add(new EditorContents
                {
                    Index = groups.Count,
                    Id = GroupViewComponent.Name + Guid.NewGuid().ToString("N"),
                    Gap = isMobile == true ? groups.Any() : false,
                    Header = groupBlock.Content.Header,
                    ParentKey = aside.Key,
                    EditorComponent = GroupViewComponent.Name,
                    Defaults = new EditorDefaults
                    {
                        CardStyleImageLocation = EditorCardStyleImageLocations.Hide,
                        CardStyleHeader = EditorCardStyleHeaders.Hide,
                        CardStyleText = EditorCardStyleTexts.Hide,
                        CardStyleAction = ActionStyles.Link,
                        CardStyleActionClickArea = ActionStyleClickAreas.Action,
                        CardStyleActionAlignment= ActionStyleAlignments.RightAbsolute
                    },
                    Block = block
                });
            }
            else
            {
                if (items == null)
                {
                    items = new List<BlockListItem>();
                }
                items.Add(block);
            }
        }

        AddItemsToGroup();
        return groups;
    }

    public override IActionResult Index()
    {
        var model = CurrentPage as EditorPage 
            ?? throw new ApplicationException("This controller is for " + nameof(EditorPage) + " only");

        var content = editorService.Load(null, null, model.Body);

        if (content?.Contents.Any() != true)
        {
            return Content("");
        }

        var offset = (int) (viewService.CurrentAside?.AsideOffset ?? 1);
        if (offset < 1 || offset > 5)
        {
            offset = 1;
        }

        Tuple<bool, IEnumerable<IEditorContent>>? aboveContent = null;
        Tuple<bool, IEnumerable<IEditorContent>>? belowContent = null;

        if (viewService.CurrentAside?.AsideStatus == AsideStatuses.Left || viewService.CurrentAside?.AsideStatus == AsideStatuses.Right)
        {
            if (offset == 1)
            {
                belowContent = new Tuple<bool, IEnumerable<IEditorContent>>(false, content.Contents);
            }
            else
            {
                aboveContent = new Tuple<bool, IEnumerable<IEditorContent>>(true, content.Contents.Take(offset - 1));
                belowContent = new Tuple<bool, IEnumerable<IEditorContent>>(false, content.Contents.Skip(offset - 1));
            }
        }
        else
        {
            aboveContent = new Tuple<bool, IEnumerable<IEditorContent>>(true, content.Contents);
        }

        return CurrentTemplate(new EditorPageViewModel(model, publishedValueFallback)
        {
            Breadcrumbs = viewService.CurrentFurniture.BreadcrumbStatusTyped == BreadcrumbStatuses.Show 
                ? model.Ancestors<EditorPage>().Reverse().Select(p => new Tuple<string, string>(string.IsNullOrWhiteSpace(p.Title) ? p.Name : p.Title, p.Url())) 
                : null,
            BreadcrumbSubtheme = viewService.CurrentFurniture.BreadcrumbSubthemeTyped,
            BreadcrumbThemeShade = viewService.CurrentFurniture.BreadcrumbThemeShadeTyped,
            BreadcrumbsColor = viewService.CurrentFurniture.FurnitureBreadcrumbsColor,
            PageTitleSubtheme = model.PageTitleSubthemeTyped == EditorSubthemes.Inherit ? viewService.CurrentFurniture.HeaderSubthemeTyped : model.PageTitleSubthemeTyped,
            PageTitleThemeShade = model.PageTitleThemeShadeTyped == EditorThemeShades.Inherit ? viewService.CurrentFurniture.HeaderThemeShadeTyped : model.PageTitleThemeShadeTyped,
            Alert = viewService.CurrentAlert,
            Contents = content!.Contents,
            AboveContents = aboveContent,
            BelowContents = belowContent,
            PageLayout = viewService.CurrentAside?.AsideStatusTyped ?? AsideStatuses.Hide,
            AsideDesktop = AsideToGroups(viewService.CurrentAside, false),
            AsideMobile = AsideToGroups(viewService.CurrentAside, true),
            Subfooter = viewService.CurrentSubfooter,
            Theme = viewService.CurrentTheme,
            Subtheme = viewService.CurrentSubtheme,
            ThemeShade = viewService.CurrentThemeShade
        });
    }
}
