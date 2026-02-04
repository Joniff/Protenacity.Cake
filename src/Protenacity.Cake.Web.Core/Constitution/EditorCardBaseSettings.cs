using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IEditorCardBaseSettings
{
    EditorCardStyleImageLocations StyleImageLocationTyped { get; }
    EditorCardStyleImageSizes StyleImageSizeTyped { get; }
    EditorCardStyleHeaders StyleHeaderTyped { get; }
    EditorCardStyleDates StyleDateTyped { get; }
    EditorCardStyleTimes StyleTimeTyped { get; }
    EditorCardStyleTexts StyleTextTyped { get; }
}

public partial class EditorCardBaseSettings
{
    public EditorCardStyleImageLocations StyleImageLocationTyped => EditorCardStyleImageLocations.ParseByDescription(this.StyleImage);
    public EditorCardStyleImageSizes StyleImageSizeTyped => EditorCardStyleImageSizes.ParseByDescription(this.StyleImageSize);
    public EditorCardStyleHeaders StyleHeaderTyped => EditorCardStyleHeaders.ParseByDescription(this.StyleHeader);
    public EditorCardStyleDates StyleDateTyped => EditorCardStyleDates.ParseByDescription(this.StyleDate);
    public EditorCardStyleTimes StyleTimeTyped => EditorCardStyleTimes.ParseByDescription(this.StyleTime);
    public EditorCardStyleTexts StyleTextTyped => EditorCardStyleTexts.ParseByDescription(this.StyleText);
}