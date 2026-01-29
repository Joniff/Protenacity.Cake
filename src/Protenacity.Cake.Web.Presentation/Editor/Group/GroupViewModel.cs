namespace Protenacity.Cake.Web.Presentation.Editor.Group;

public class GroupViewModel : EditorContents
{
    public required IEnumerable<IEditorContent> Contents { get; init; }
}
