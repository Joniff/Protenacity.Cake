using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.ExpandableSection;

public class ExpandableSectionViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(ExpandableSection);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        if (content == null)
        {
            return Content(string.Empty);
        }

        var body = editorService.Load(null, content.Defaults, (content.Block?.Content as EditorExpandableSectionPrimary)?.Body);

        if (body?.Contents.Any() != true)
        {
            // We have no content to show
            return Content(string.Empty);
        }

        var settings = content.Block?.Settings as EditorExpandableSectionPrimarySettings;

        return View(new ExpandableSectionViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            Body = body.Contents,
            Subtheme = settings?.Subtheme ?? EditorSubthemes.Inherit,
            ThemeShades = settings?.ThemeShade ?? EditorThemeShades.Inherit,
            CollapsedSize = settings?.CollapsedSizeValue ?? 0,
            CollapsedSizeUnit = settings?.CollapsedSizeUnit ?? EditorExpandableSectionCollapseSizeUnits.SectionPercentage,
            ExpandCollapseMethod = settings?.ExpandCollapseMethod ?? EditorExpandableSectionExpandCollapseMethods.Arrow,
            InitialState = settings?.InitialState ?? EditorExpandableSectionInitialStates.Collapsed
        });
    }
}
