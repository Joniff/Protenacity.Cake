using Protenacity.Cake.Web.Core.Constitution;
using Microsoft.AspNetCore.Mvc;

namespace Protenacity.Cake.Web.Presentation.Editor.List;

public class ListViewComponent(IEditorService editorService) : ViewComponent
{
    public const string Name = nameof(List);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var cardSettings = content.Block?.Settings as IEditorNoteDefaultSettings;

        if (cardSettings != null)
        {
            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardImage))
            {
                content.Defaults.CardStyleImageLocation = cardSettings.StyleCardImageLocationTyped;
            }

            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardImageSize))
            {
                content.Defaults.CardStyleImageSize = cardSettings.StyleCardImageSizeTyped;
            }

            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardHeader))
            {
                content.Defaults.CardStyleHeader = cardSettings.StyleCardHeaderTyped;
            }

            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardDate))
            {
                content.Defaults.CardStyleDate = cardSettings.StyleCardDateTyped;
            }

            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardTime))
            {
                content.Defaults.CardStyleTime = cardSettings.StyleCardTimeTyped;
            }

            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardText))
            {
                content.Defaults.CardStyleText = cardSettings.StyleCardTextTyped;
            }

            if (cardSettings.StyleCardSubthemeTyped != Core.Property.EditorSubthemes.Inherit)
            {
                content.Defaults.CardStyleSubtheme = cardSettings.StyleCardSubthemeTyped;
            }

            if (cardSettings.StyleCardThemeShadeTyped != Core.Property.EditorThemeShades.Inherit)
            {
                content.Defaults.CardStyleThemeShade = cardSettings.StyleCardThemeShadeTyped;
            }

            if (cardSettings.StyleCardOverrideColor?.Any() == true)
            {
                content.Defaults.CardStyleOverrideColor = cardSettings.StyleCardOverrideColor;
            }

            if (!string.IsNullOrWhiteSpace(cardSettings.StyleCardBorderColor?.Color))
            {
                content.Defaults.CardStyleBorderColor = cardSettings.StyleCardBorderColor.Color;
            }

        }

        var actionSettings = content.Block?.Settings as IEditorActionDefaultSettings;

        if (actionSettings != null)
        {
            if (!string.IsNullOrWhiteSpace(actionSettings.StyleAction))
            {
                content.Defaults.CardStyleAction = actionSettings.StyleActionTyped;
            }

            if (!string.IsNullOrWhiteSpace(actionSettings.StyleActionClickArea))
            {
                content.Defaults.CardStyleActionClickArea = actionSettings.StyleActionClickAreaTyped;
            }

            if (!string.IsNullOrWhiteSpace(actionSettings.StyleActionAlignment))
            {
                content.Defaults.CardStyleActionAlignment = actionSettings.StyleActionAlignmentTyped;
            }
        }

        var blocks = editorService.Load(null, content.Defaults, (content.Block?.Content as IEditorListEmbedded)?.ListBlocks);

        if (blocks?.Contents.Any() != true)
        {
            // We have no blocks to show
            return Content(string.Empty);
        }

        return View(new ListViewModel
        {
            Id = Name + Guid.NewGuid().ToString("N"),
            ListType = (content.Block?.Settings as EditorListPrimarySettings)?.ListTypeTyped ?? Core.Property.ListTypes.Grid,
            MaxColumns = (content.Block?.Settings as IEditorListBaseSettings)?.MaxColumns ?? 3,
            Blocks = blocks.Contents,
            Paging = blocks.Paging
        });
    }
}
