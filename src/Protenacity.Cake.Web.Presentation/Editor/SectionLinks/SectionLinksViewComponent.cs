using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.Action;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.SectionLinks;

public class SectionLinksViewComponent(IViewService viewService, 
    IEditorService editorService) 
    : ThemeViewComponent
{
    public const string Name = nameof(SectionLinks);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var links = new List<ActionViewModel>();
        var page = viewService.CurrentPage as EditorPage;
        var settings = content.Block?.Settings as EditorSectionLinksPrimarySettings;

        if (page == null)
        {
            return Content(string.Empty);
        }

        var contentPaging = editorService.Load(null, null, page.Body);

        if (contentPaging?.Contents.Any() != true)
        {
            return Content("");
        }

        foreach (var section in contentPaging.Contents)
        {
            if (settings?.EnableThisSection == false && section.Id == content.Id)
            {
                continue;
            }

            links.Add(new ActionViewModel
            {
                Style = ActionStyles.Link,
                Name = section.Header,
                Target = ActionTargets.CurrentTab,
                Url = "#" + section.Id,
                Download = false,
                TextRestrict = content.Defaults.TextRestrict
            });
        }

        return View(new SectionLinksViewModel
        {
            Actions = links,
            Subtheme = Subtheme(content),
            ThemeShade = ThemeShade(content),
            OverrideColor = OverrideColor(content),
            BorderColor = BorderColor(content),
            BorderEdges = BorderEdges(content)
        });
    }
}
