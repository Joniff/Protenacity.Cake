using Protenacity.Cake.Web.Core.Constitution;

namespace Protenacity.Cake.Web.Presentation.Editor.Section;

public class SectionViewModel : EditorContent
{
    public IEditorBlockPrimary Primary => Block?.Content as IEditorBlockPrimary
        ?? throw new ApplicationException(Block?.Content?.GetType().FullName + " has to be of type " + nameof(IEditorBlockPrimary));
    public IEditorBlockPrimarySettings PrimarySettings => Block?.Settings as IEditorBlockPrimarySettings
        ?? throw new ApplicationException(Block?.Settings?.GetType().FullName + " has to be of type " + nameof(IEditorBlockPrimarySettings));
}
