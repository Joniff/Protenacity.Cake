using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.List;

public class ListViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(List);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var cardSettings = content.Block?.Settings as IEditorCardDefaultSettings;

        if (cardSettings?.StyleCardImage != null)
        {
            content.Defaults.CardStyleImageLocation = cardSettings.StyleCardImage ?? Core.Property.EditorCardStyleImageLocations.Top;
        }

        if (cardSettings?.StyleCardImageSize != null)
        {
            content.Defaults.CardStyleImageSize = cardSettings.StyleCardImageSize ?? Core.Property.EditorCardStyleImageSizes.Medium;
        }

        if (cardSettings?.StyleCardHeader != null)
        {
            content.Defaults.CardStyleHeader = cardSettings.StyleCardHeader ?? Core.Property.EditorCardStyleHeaders.Show;
        }

        if (cardSettings?.StyleCardDate != null)
        {
            content.Defaults.CardStyleDate = cardSettings.StyleCardDate ?? Core.Property.EditorCardStyleDates.Show;
        }

        if (cardSettings?.StyleCardTime != null)
        {
            content.Defaults.CardStyleTime = cardSettings.StyleCardTime ?? Core.Property.EditorCardStyleTimes.Show;
        }

        if (cardSettings?.StyleCardText != null)
        {
            content.Defaults.CardStyleText = cardSettings.StyleCardText ?? Core.Property.EditorCardStyleTexts.Show;
        }

        if ((cardSettings?.StyleCardSubtheme ?? Core.Property.EditorSubthemes.Inherit) != Core.Property.EditorSubthemes.Inherit)
        {
            content.Defaults.CardStyleSubtheme = cardSettings!.StyleCardSubtheme ?? Core.Property.EditorSubthemes.Primary;
        }

        if ((cardSettings?.StyleDefaultCardThemeShade ?? Core.Property.EditorThemeShades.Inherit) != Core.Property.EditorThemeShades.Inherit)
        {
            content.Defaults.CardStyleThemeShade = cardSettings!.StyleDefaultCardThemeShade ?? Core.Property.EditorThemeShades.Light;
        }

        if (cardSettings?.StyleCardOverrideColor?.Any() == true)
        {
            content.Defaults.CardStyleOverrideColor = cardSettings.StyleCardOverrideColor;
        }

        if (!string.IsNullOrWhiteSpace(cardSettings?.StyleCardBorderColor?.Color))
        {
            content.Defaults.CardStyleBorderColor = cardSettings.StyleCardBorderColor.Color;
        }

        var actionSettings = content.Block?.Settings as IEditorActionDefaultSettings;

        if (actionSettings?.StyleAction != null)
        {
            content.Defaults.CardStyleAction = actionSettings.StyleAction ?? Core.Property.ActionStyles.Button;
        }

        if (actionSettings?.StyleActionClickArea != null)
        {
            content.Defaults.CardStyleActionClickArea = actionSettings.StyleActionClickArea ?? Core.Property.ActionStyleClickAreas.Action;
        }

        if (actionSettings?.StyleActionAlignment != null)
        {
            content.Defaults.CardStyleActionAlignment = actionSettings.StyleActionAlignment ?? Core.Property.ActionStyleAlignments.Right;
        }

        var blocks = editorService.Load(null, content.Defaults, (content.Block?.Content as IEditorListEmbedded)?.ListBlocks);

        if (blocks?.Contents.Any() != true)
        {
            // We have no blocks to show
            return Content(string.Empty);
        }

        var listbaseSettings = content.Block?.Settings as IEditorListBaseSettings;

        return View(new ListViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            ListType = (content.Block?.Settings as EditorListPrimarySettings)?.ListType ?? Core.Property.EditorListTypes.Grid,
            MinColumns = listbaseSettings != null && listbaseSettings.MinColumns > 0 ? listbaseSettings.MinColumns : 1,
            MaxColumns = listbaseSettings != null && listbaseSettings.MaxColumns > 0 ? listbaseSettings.MaxColumns : 3,
            Blocks = blocks.Contents,
            Paging = blocks.Paging
        });
    }
}
