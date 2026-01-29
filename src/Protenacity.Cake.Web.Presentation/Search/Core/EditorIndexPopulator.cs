using Examine;
using Protenacity.Cake.Web.Presentation.Search.BackgroundTask;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.Examine;

namespace Protenacity.Cake.Web.Presentation.Search.Core;

public class EditorIndexPopulator : IndexPopulator
{
    private readonly IEditorSearchBackgroundTask editorSearchBackgroundTask;

    public EditorIndexPopulator(IEditorSearchBackgroundTask _editorSearchBackgroundTask)
    {
        editorSearchBackgroundTask = _editorSearchBackgroundTask;
        RegisterIndex(nameof(EditorIndex));
    }

    protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
    {
        if (indexes.Any(i => i.Name.Equals(nameof(EditorIndex))))
        {
            editorSearchBackgroundTask.ExectionOfAllContentNow();
        }
    }
}
