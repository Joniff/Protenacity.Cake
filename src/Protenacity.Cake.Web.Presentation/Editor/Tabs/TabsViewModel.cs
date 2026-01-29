using Protenacity.Cake.Web.Core.Constitution;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Tabs;

public class TabsViewModel
{
    public required string Id { get; init; }
    public required BlockListItem<EditorTabsPrimary, EditorTabsPrimarySettings> Tabs { get; init; }
    public required IEnumerable<IEditorContent> Panels { get; init; }
}
