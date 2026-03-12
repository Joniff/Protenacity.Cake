using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.Editor.ResponsiveImage;

namespace Protenacity.Cake.Web.Presentation.Editor.Card;

public abstract class BaseViewComponent() : ThemeViewComponent
{
    protected const string Template = "~/Views/Components/Card/Default.cshtml";
    protected const string TopTemplate = "~/Views/Components/Card/Top.cshtml";
    protected const string BottomTemplate = "~/Views/Components/Card/Bottom.cshtml";
    protected const string LeftTemplate = "~/Views/Components/Card/Left.cshtml";
    protected const string RightTemplate = "~/Views/Components/Card/Right.cshtml";
    protected const string BehindTemplate = "~/Views/Components/Card/Behind.cshtml";
    protected const string NoImageTemplate = "~/Views/Components/Card/NoImage.cshtml";

    protected string GetTemplate(bool hasImages, EditorCardStyleImageLocations styleImage)
    {
        if (!hasImages)
        {
            return NoImageTemplate;
        }

        switch (styleImage)
        {
            case EditorCardStyleImageLocations.Hide:
                return NoImageTemplate;

            case EditorCardStyleImageLocations.Top:
                return TopTemplate;

            case EditorCardStyleImageLocations.Bottom:
                return BottomTemplate;

            case EditorCardStyleImageLocations.Left:
                return LeftTemplate;

            case EditorCardStyleImageLocations.Right:
                return RightTemplate;

            case EditorCardStyleImageLocations.Behind:
                return BehindTemplate;

        }

        throw new ApplicationException("Unknown Image Location Style");
    }

    protected ResponseImageRoundedEdges GetRoundedEdges(bool hasImages, EditorCardStyleImageLocations styleImage)
    {
        if (!hasImages)
        {
            return ResponseImageRoundedEdges.None;
        }

        switch (styleImage)
        {
            case EditorCardStyleImageLocations.Hide:
                return ResponseImageRoundedEdges.None;

            case EditorCardStyleImageLocations.Top:
                return ResponseImageRoundedEdges.Top;

            case EditorCardStyleImageLocations.Bottom:
                return ResponseImageRoundedEdges.Bottom;

            case EditorCardStyleImageLocations.Left:
                return ResponseImageRoundedEdges.Left;

            case EditorCardStyleImageLocations.Right:
                return ResponseImageRoundedEdges.Right;

            case EditorCardStyleImageLocations.Behind:
                return ResponseImageRoundedEdges.All;

        }

        throw new ApplicationException("Unknown Image Location Style");
    }
}



