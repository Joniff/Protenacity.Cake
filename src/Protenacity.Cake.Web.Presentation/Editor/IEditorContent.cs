using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor;

public interface IEditorContent
{
    int Index { get; }
    string Id { get; }
    IHtmlEncodedString? Header { get; }
    string EditorComponent { get; }
    BlockListItem? Block { get; }
    EditorDefaults Defaults { get; }
}

public interface IEditorContent<T> : IEditorContent
{
    T? ExtraData { get; init; }
}
