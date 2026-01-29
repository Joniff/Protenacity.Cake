using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.Category;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Category;

public class CategoryViewComponent(ICategoryService categoryService, 
    IViewService viewService) 
    : ThemeViewComponent
{
    public const string Name = nameof(Category);

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var category = content.Block?.Content as IEditorCategoriesEmbedded;
        var settings = content.Block?.Settings as IEditorCategoriesBaseSettings;

        if (category?.Categories?.Any() != true)
        {
            return Content("");
        }

        return View(new CategoryViewModel
        {
            Id = Guid.NewGuid().ToString("N"),
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content),
            Heading = categoryService.GetCategories(viewService.CurrentDomainPage, category.Categories),
            Placement = settings?.PlacementTyped ?? Core.Property.EditorTabStripPlacements.Top,
            ShowHeadingDescription = settings?.ShowHeadingDescription ?? true,
            ShowSeparator = settings?.Separator ?? true
        });
    }
}
