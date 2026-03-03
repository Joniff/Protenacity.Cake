using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockCodeViewModel
{
    public required string Id { get; init; }
    public required EditorCodeLanguage Language { get; init; }
    public required string Text { get; init; }
    public EditorSubthemes Subtheme { get; init; }
    public EditorThemeShades Shade { get; init; }
    public BlockListModel? OverrideColor { get; init; }
    public bool EnableSyntaxHighlighting { get; init; }
    public bool AddDependencyLibrary { get; init; }
    public bool AddDependencyLibraryLanguage { get; init; }
}
