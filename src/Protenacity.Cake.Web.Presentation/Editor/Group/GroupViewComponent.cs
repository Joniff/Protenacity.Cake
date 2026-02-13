using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.Group;

public class GroupViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(Group);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContents content)
    {
        if (content == null)
        {
            return Content(string.Empty);
        }

        var contentDefaults = content.Defaults ?? new EditorDefaults();
        if ((content.Block?.Settings as EditorGroupAsideSettings)?.TextRestrict == Core.Property.EditorTextRestricts.WordWrap)
        {
            contentDefaults.TextRestrict = Core.Property.EditorTextRestricts.WordWrap;
        }
        var children = editorService.Load(content.ParentKey, contentDefaults, (content.Block?.Content as EditorGroupAside)?.Contents ?? content.Blocks);

        if (children?.Contents.Any() == true)
        {
            return View(new Tuple<IEditorContents, IEnumerable<IEditorContent>>(content, children.Contents));
        }

        return Content(string.Empty);
    }
}
