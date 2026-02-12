using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Umbraco.Cms.Core.Models.Blocks;

namespace Protenacity.Cake.Web.Presentation.Editor;

public abstract class ThemeViewComponent
    : ViewComponent
{
    public EditorCardStyleImageLocations StyleImageLocation([DisallowNull] IEditorContent content) => 
        (content.Block?.Settings as IEditorCardBaseSettings)?.StyleImage ?? content.Defaults.CardStyleImageLocation;

    public EditorCardStyleImageSizes StyleImageSize([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorCardBaseSettings)?.StyleImageSize ?? content.Defaults.CardStyleImageSize;

    public EditorCardStyleHeaders StyleHeader([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorCardBaseSettings)?.StyleHeader ?? content.Defaults.CardStyleHeader;

    public EditorCardStyleDates StyleDate([DisallowNull] IEditorContent content) => 
        (content.Block?.Settings as IEditorCardBaseSettings)?.StyleDate ?? content.Defaults.CardStyleDate;

    public EditorCardStyleTimes StyleTime([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorCardBaseSettings)?.StyleTime ?? content.Defaults.CardStyleTime;

    public EditorCardStyleTexts StyleText([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorCardBaseSettings)?.StyleText ?? content.Defaults.CardStyleText;

    public ActionStyles StyleAction([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorActionEmbeddedSettings)?.StyleAction ?? content.Defaults.CardStyleAction;

    public ActionStyleClickAreas StyleActionClickArea([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorActionEmbeddedSettings)?.StyleActionClickArea ?? content.Defaults.CardStyleActionClickArea;

    public ActionStyleAlignments StyleActionAlignment([DisallowNull] IEditorContent content) =>
        (content.Block?.Settings as IEditorActionEmbeddedSettings)?.StyleActionAlignmentTyped ?? content.Defaults.CardStyleActionAlignment;

    public bool StyleShowClickArrow([DisallowNull] IEditorContent content) => (content.Block?.Settings as IEditorCardBaseSettings)?.ShowClickArrow == true;

    private string SubthemeKey => nameof(ThemeViewComponent) + nameof(EditorSubthemes);

    public EditorSubthemes Subtheme()
    {
        var obj = TempData[SubthemeKey];
        if (obj != null)
        {
            return (EditorSubthemes)obj;
        }
        return EditorSubthemes.Inherit;
    }

    public EditorSubthemes Subtheme([DisallowNull] IEditorContent content)
    {
        var background = content.Block?.Settings as IEditorBackgroundSettings;
        if (background != null && background.SubthemeTyped != EditorSubthemes.Inherit)
        {
            TempData[SubthemeKey] = (int)background.SubthemeTyped;
            return background.SubthemeTyped;
        }
        //var obj = TempData[SubthemeKey];
        //if (obj != null)
        //{
        //    return (EditorSubthemes)obj;
        //}
        TempData[SubthemeKey] = (int)content.Defaults.CardStyleSubtheme;
        return content.Defaults.CardStyleSubtheme;
    }

    private string ThemeShadeKey = nameof(ThemeViewComponent) + nameof(EditorThemeShades);

    public EditorThemeShades ThemeShade()
    {
        var obj = TempData[ThemeShadeKey];
        if (obj != null)
        {
            return (EditorThemeShades)obj;
        }
        return EditorThemeShades.Inherit;
    }

    public EditorThemeShades ThemeShade([DisallowNull] IEditorContent content)
    {
        var background = content.Block?.Settings as IEditorBackgroundSettings;
        if (background != null && background.ThemeShadeTyped != EditorThemeShades.Inherit)
        {
            TempData[ThemeShadeKey] = (int)background.ThemeShadeTyped;
            return background.ThemeShadeTyped;
        }
        //var obj = TempData[ThemeShadeKey];
        //if (obj != null)
        //{
        //    return (EditorThemeShades)obj;
        //}
        TempData[ThemeShadeKey] = (int)content.Defaults.CardStyleThemeShade;
        return content.Defaults.CardStyleThemeShade;
    }

    public BlockListModel? OverrideColor([DisallowNull] IEditorContent content) => (content.Block?.Settings as IEditorBackgroundSettings)?.OverrideColor?.Any() == true
        ? (content.Block?.Settings as IEditorBackgroundSettings)?.OverrideColor
        : content.Defaults.CardStyleOverrideColor;

    public string? BorderColor([DisallowNull] IEditorContent content) => string.IsNullOrWhiteSpace((content.Block?.Settings as IEditorBorderSettings)?.BorderColor?.Color)
        ? content.Defaults.CardStyleBorderColor
        : (content.Block?.Settings as IEditorBorderSettings)?.BorderColor?.Color;

    public EditorBorderEdges BorderEdges([DisallowNull] IEditorContent content)
    {
        var borderEdges = (content.Block?.Settings as IEditorBorderSettings)?.BorderEdges;
        if (borderEdges != null && borderEdges != EditorBorderEdges.None)
        {
            return (EditorBorderEdges)borderEdges;
        }

        return content.Defaults.CardStyleBorderEdges != EditorBorderEdges.None ? content.Defaults.CardStyleBorderEdges : EditorBorderEdges.All;
    }

    public int PageSize([DisallowNull] IEditorContent content, int defaultPageSize, int maximumPageSize)
    {
        var pageSizeSettings = content.Block?.Settings as IEditorPageSizeSettings;
        var pageSize = pageSizeSettings?.PageSize ?? defaultPageSize;
        if (pageSize < 1 || pageSize > maximumPageSize)
        {
            pageSize = defaultPageSize;
        }
        return pageSize;
    }
}
