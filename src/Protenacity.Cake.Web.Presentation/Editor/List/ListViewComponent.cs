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
            content.Defaults.CardStyleImageLocation = cardSettings.StyleCardImage;
        }

        if (cardSettings?.StyleCardImageSize != null)
        {
            content.Defaults.CardStyleImageSize = cardSettings.StyleCardImageSize;
        }

        if (cardSettings?.StyleCardHeader != null)
        {
            content.Defaults.CardStyleHeader = cardSettings.StyleCardHeader;
        }

        if (cardSettings?.StyleCardDate != null)
        {
            content.Defaults.CardStyleDate = cardSettings.StyleCardDate;
        }

        if (cardSettings?.StyleCardTime != null)
        {
            content.Defaults.CardStyleTime = cardSettings.StyleCardTime;
        }

        if (cardSettings?.StyleCardText != null)
        {
            content.Defaults.CardStyleText = cardSettings.StyleCardText;
        }

        if ((cardSettings?.StyleCardSubthemeTyped ?? Core.Property.EditorSubthemes.Inherit) != Core.Property.EditorSubthemes.Inherit)
        {
            content.Defaults.CardStyleSubtheme = cardSettings!.StyleCardSubthemeTyped;
        }

        if ((cardSettings?.StyleCardThemeShadeTyped ?? Core.Property.EditorThemeShades.Inherit) != Core.Property.EditorThemeShades.Inherit)
        {
            content.Defaults.CardStyleThemeShade = cardSettings!.StyleCardThemeShadeTyped;
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
            content.Defaults.CardStyleAction = actionSettings.StyleAction;
        }

        if (actionSettings?.StyleActionClickArea != null)
        {
            content.Defaults.CardStyleActionClickArea = actionSettings.StyleActionClickArea;
        }

        if (actionSettings?.StyleActionAlignment != null)
        {
            content.Defaults.CardStyleActionAlignment = actionSettings.StyleActionAlignment;
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
            ListType = (content.Block?.Settings as EditorListPrimarySettings)?.ListTypeTyped ?? Core.Property.EditorListTypes.Grid,
            MaxColumns = (content.Block?.Settings as IEditorListBaseSettings)?.MaxColumns ?? 3,
            Blocks = blocks.Contents,
            Paging = blocks.Paging
        });
    }
}
