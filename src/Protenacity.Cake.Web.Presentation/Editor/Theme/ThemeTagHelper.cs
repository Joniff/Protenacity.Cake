using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Protenacity.Cake.Web.Presentation.Editor.Background;

[HtmlTargetElement("theme")]
public class ThemeTagHelper(IViewService viewService, IResponsiveImageService responsiveImageService) : TagHelper
{
    private const string ClassAttribute = "class";
    private const string StyleAttribute = "style";

    [HtmlAttributeName("theme")]
    public EditorThemes? Theme { get; set; }

    [HtmlAttributeName("subtheme")]
    public EditorSubthemes? Subtheme { get; set; }

    [HtmlAttributeName("shade")]
    public EditorThemeShades? Shade { get; set; }

    [HtmlAttributeName("override")]
    public global::Umbraco.Cms.Core.Models.Blocks.BlockListModel? Override { get; set; }

    [HtmlAttributeName("border-color")]
    public string? BorderColor { get; set; }

    [HtmlAttributeName("border-edges")]
    public EditorBorderEdges BorderEdges { get; set; }

    [HtmlAttributeName("disable-override-background")]
    public bool DisableOverrideBackground { get; set; }

    [HtmlAttributeName("disable-override-text")]
    public bool DisableOverrideText { get; set; }

    [HtmlAttributeName("disable-border")]
    public bool DisableBorder { get; set; }

    [HtmlAttributeName("tag")]
    public string Tag { get; set; } = "div";

    private string GradientAngle(EditorGradientTypes type)
    {
        switch (type)
        {
            case EditorGradientTypes.Top:
                return "to bottom";

            case EditorGradientTypes.Left:
                return "to right";

            case EditorGradientTypes.TopLeft:
                return "to bottom right";

            case EditorGradientTypes.Centre:
                return "circle";

            default:
                throw new ApplicationException("Unknown " + nameof(EditorGradientTypes));
        }
    }

    private static string Opacity(int opacity)
    {
        if (opacity == 0)
        {
            return string.Empty;
        }
        else if (opacity == -10)
        {
            return "linear-gradient(rgba(255, 255, 255, 1), rgba(255, 255, 255, 1)),";
        }
        if (opacity == 10)
        {
            return "linear-gradient(rgba(0, 0, 0, 1), rgba(0, 0, 0, 1)),";
        }
        else if (opacity < 0)
        {
            var abs = int.Abs(opacity);
            return "linear-gradient(rgba(255, 255, 255, 0." + abs + "), rgba(255, 255, 255, 0." + abs + ")),";
        }
        return "linear-gradient(rgba(0, 0, 0, 0." + opacity + "), rgba(0, 0, 0, 0." + opacity + ")),";
    }

    private string ThemeClass(EditorThemes theme)
    {
        switch (theme)
        {
            case EditorThemes.Default:
                return "theme-default";

            case EditorThemes.Venice:
                return "theme-venice";

            case EditorThemes.MetallicSeaweed:
                return "theme-metallic-seaweed";

            case EditorThemes.DeepDairei:
                return "theme-deep-dairei";

            case EditorThemes.CactusFlower:
                return "theme-cactus-flower";

            case EditorThemes.MysticTulip:
                return "theme-mystic-tulip";

            case EditorThemes.Poinciana:
                return "theme-poinciana";
        }
        throw new ArgumentException(nameof(EditorThemes) + " invalid value of " + theme);
    }

    private string SubthemeClass(EditorSubthemes subtheme)
    {
        switch (subtheme) 
        {
            case EditorSubthemes.Primary:
                return "theme-primary";

            case EditorSubthemes.Secondary:
                return "theme-secondary";

            case EditorSubthemes.Tertiary:
                return "theme-tertiary";
        }
        throw new ArgumentException(nameof(EditorSubthemes) + " invalid value of " + subtheme);
    }

    private string ThemeShadeClass(EditorThemeShades themeShade)
    {
        switch (themeShade)
        {
            case EditorThemeShades.Light:
                return "theme-light";

            case EditorThemeShades.Dark:
                return "theme-dark";
        }
        throw new ArgumentException(nameof(EditorThemeShades) + " invalid value of " + themeShade);
    }

    private void AddOriginalValues(TagHelperOutput output, string attributeName, ref List<string> attributeValues)
    {
        var original = output.Attributes[attributeName]?.Value as Microsoft.AspNetCore.Html.HtmlString;
        if (original != null)
        {
            attributeValues.Add(original.ToString());
        }
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = Tag;

        var attributeClass = new List<string>();
        AddOriginalValues(output, ClassAttribute, ref attributeClass);

        var attributeStyle = new List<string>();
        AddOriginalValues(output, StyleAttribute, ref attributeStyle);

        //  Theme
        if (Theme != null && Theme != EditorThemes.Inherit)
        {
            attributeClass.Add(ThemeClass((EditorThemes)Theme));
        }

        //  Subtheme
        if (Subtheme != null && Subtheme != EditorSubthemes.Inherit)
        {
            attributeClass.Add(SubthemeClass((EditorSubthemes)Subtheme));
        }

        //  Shade
        if (Shade != null && Shade != EditorThemeShades.Inherit)
        {
            attributeClass.Add(ThemeShadeClass((EditorThemeShades)Shade));
        }

        // Border
        if (!string.IsNullOrWhiteSpace(BorderColor) && BorderEdges != EditorBorderEdges.None && !DisableBorder)
        {
            attributeClass.Add("border-5");
            if (BorderEdges == EditorBorderEdges.All)
            {
                attributeClass.Add("border rounded");
            }
            else
            {
                if ((BorderEdges & EditorBorderEdges.Top) == EditorBorderEdges.Top)
                {
                    attributeClass.Add("border-top");
                }
                if ((BorderEdges & EditorBorderEdges.Bottom) == EditorBorderEdges.Bottom)
                {
                    attributeClass.Add("border-bottom");
                }
                if ((BorderEdges & EditorBorderEdges.Left) == EditorBorderEdges.Left)
                {
                    attributeClass.Add("border-start");
                }
                if ((BorderEdges & EditorBorderEdges.Right) == EditorBorderEdges.Right)
                {
                    attributeClass.Add("border-end");
                }
            }
            attributeStyle.Add("border-color:" + BorderColor + " !important;");
        }

        // Text
        var overrideColors = Override?.FirstOrDefault();
        if (overrideColors != null)
        {
            var text = (overrideColors?.Content as IEditorBlockPrimarySettingsTextColor)?.ForegroundColor?.Color;
            if (!string.IsNullOrWhiteSpace(text) && !DisableOverrideText)
            {
                attributeStyle.Add("color:" + text + " !important;");
            }

            if (!DisableOverrideBackground)
            {
                switch (overrideColors?.Content.ContentType.Alias)
                {
                    case EditorBlockPrimarySettingsBackgroundColor.ModelTypeAlias:
                        {
                            var source = overrideColors.Content as EditorBlockPrimarySettingsBackgroundColor
                                ?? throw new ApplicationException(nameof(overrideColors.Content) + " should be of type " + nameof(EditorBlockPrimarySettingsBackgroundColor));

                            //< div class="@(Model.Expand ? "expand-background" : null) @(Model.Border != null ? "border border-5 rounded" : null)" style="background-color:@Model.Color;@(Model.Border != null ? "border-color:" + Model.Border + "!important;" : null)>

                            attributeStyle.Add("background-color:" + source.BackgroundColor + ";");
                        }
                        break;

                    case EditorBlockPrimarySettingsBackgroundImage.ModelTypeAlias:
                        {
                            var source = overrideColors.Content as EditorBlockPrimarySettingsBackgroundImage
                                ?? throw new ApplicationException(nameof(overrideColors.Content) + " should be of type " + nameof(EditorBlockPrimarySettingsBackgroundImage));

                            var id = output.TagName + Guid.NewGuid().ToString("N");
                            var urls = responsiveImageService.ImageUrls(source.Image, Core.Property.EditorImageCrops.Full, 100, viewService.CurrentDomainPage.ConfigImageQuality);
                            var opacity = Opacity((int)source.Opacity);

                            output.PreElement.AppendHtmlLine("<style>." + id + "{");
                            output.PreElement.AppendHtml("background:");
                            output.PreElement.AppendHtml(opacity);
                            output.PreElement.AppendHtmlLine("url('" + urls.FirstOrDefault(u => u.Item1 == null)?.Item2 + "');");
                            output.PreElement.AppendHtmlLine("background-repeat:no-repeat;");
                            output.PreElement.AppendHtmlLine("background-size:cover;");
                            output.PreElement.AppendHtmlLine("}");

                            foreach (var url in urls.Where(u => u.Item1 != null))
                            {
                                output.PreElement.AppendHtmlLine("@media (min-width:" + url.Item1 + "px) {");
                                output.PreElement.AppendHtmlLine("." + id + "{");
                                output.PreElement.AppendHtml("background:");
                                output.PreElement.AppendHtml(opacity);
                                output.PreElement.AppendHtmlLine("url('" + url.Item2 + "');");
                                output.PreElement.AppendHtmlLine("background-repeat:no-repeat;");
                                output.PreElement.AppendHtmlLine("background-size:cover;");
                                output.PreElement.AppendHtmlLine("}}");
                            }
                            output.PreElement.AppendHtmlLine("</style>");
                            attributeClass.Add(id);
                        }
                        break;

                    case EditorBlockPrimarySettingsBackground2ColorGradient.ModelTypeAlias:
                        {
                            var source = overrideColors.Content as EditorBlockPrimarySettingsBackground2ColorGradient
                                ?? throw new ApplicationException(nameof(overrideColors.Content) + " should be of type " + nameof(EditorBlockPrimarySettingsBackground2ColorGradient));

                            attributeStyle.Add("background-image:" + (source.GradientTypeTyped == EditorGradientTypes.Centre ? "radial-gradient" : "linear-gradient") + "(" +
                                GradientAngle(source.GradientTypeTyped) + "," + source.StartingColor + "," + source.EndingColor + ");");
                        }
                        break;

                    case EditorBlockPrimarySettingsBackground3ColorGradient.ModelTypeAlias:
                        {
                            var source = overrideColors.Content as EditorBlockPrimarySettingsBackground3ColorGradient
                                ?? throw new ApplicationException(nameof(overrideColors.Content) + " should be of type " + nameof(EditorBlockPrimarySettingsBackground3ColorGradient));
                            attributeStyle.Add("background-image:" + (source.GradientTypeTyped == EditorGradientTypes.Centre ? "radial-gradient" : "linear-gradient") + "(" +
                                GradientAngle(source.GradientTypeTyped) + "," + source.StartingColor + "," + source.MiddleColor + "," + source.EndingColor + ");");
                        }
                        break;

                    default:
                        throw new ApplicationException(nameof(overrideColors.Content) + " is of unknown type");
                }
            }
        }

        output.Attributes.SetAttribute(ClassAttribute, new Microsoft.AspNetCore.Html.HtmlString(string.Join(' ', attributeClass).Trim()));

        if (attributeStyle.Any())
        {
            output.Attributes.SetAttribute(StyleAttribute, new Microsoft.AspNetCore.Html.HtmlString(string.Join(';', attributeStyle).Replace(";;", ";").Trim()));
        }

        base.Process(context, output);
    }
}
