using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorNoteBaseSettings
{
    EditorCardStyleImageLocations StyleImageLocationTyped { get; }
    EditorCardStyleImageSizes StyleImageSizeTyped { get; }
    EditorCardStyleHeaders StyleHeaderTyped { get; }
    EditorCardStyleDates StyleDateTyped { get; }
    EditorCardStyleTimes StyleTimeTyped { get; }
    EditorCardStyleTexts StyleTextTyped { get; }
}

public partial class EditorNoteBaseSettings
{
    public EditorCardStyleImageLocations StyleImageLocationTyped => Enum<EditorCardStyleImageLocations>.GetValueByDescription(this.StyleImage);
    public EditorCardStyleImageSizes StyleImageSizeTyped => Enum<EditorCardStyleImageSizes>.GetValueByDescription(this.StyleImageSize);
    public EditorCardStyleHeaders StyleHeaderTyped => Enum<EditorCardStyleHeaders>.GetValueByDescription(this.StyleHeader);
    public EditorCardStyleDates StyleDateTyped => Enum<EditorCardStyleDates>.GetValueByDescription(this.StyleDate);
    public EditorCardStyleTimes StyleTimeTyped => Enum<EditorCardStyleTimes>.GetValueByDescription(this.StyleTime);
    public EditorCardStyleTexts StyleTextTyped => Enum<EditorCardStyleTexts>.GetValueByDescription(this.StyleText);
}