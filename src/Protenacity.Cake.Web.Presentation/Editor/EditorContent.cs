using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor;

public class EditorContent : IEditorContent
{
    public int Index { get; init; }
    public required string Id { get; init; }
    public IHtmlEncodedString? Header { get; init; }
    public required string EditorComponent { get; set; }
    public BlockListItem? Block { get; init; }
    public required EditorDefaults Defaults { get; init; }
}

public class EditorContent<T> : EditorContent, IEditorContent<T>
{
    public T? ExtraData { get; init; }
}
