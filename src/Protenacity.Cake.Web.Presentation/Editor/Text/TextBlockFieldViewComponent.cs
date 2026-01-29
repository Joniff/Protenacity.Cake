using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Presentation.BusRoute;
using Protenacity.Cake.Web.Presentation.View;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Presentation.Editor.Text;

public class TextBlockFieldViewComponent(IViewService viewService, IBusRouteService busRouteService) : ThemeViewComponent
{
    public const string Name = "TextBlockField";
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(RichTextBlockItem block)
    {
        var content = block.Content as EditorTextBlockField;
        var settings = block.Settings as EditorTextBlockFieldSettings;
        var getBusInfo = false;
        var getBusStopInfo = false;

        if (content == null || settings == null)
        {
            return Content(string.Empty);
        }

        string? text = null;

        switch (content.FieldTyped)
        {
            case Core.Property.EditorTextFieldTypes.Week:
                text = RichTextFields.Week;
                break;

            case Core.Property.EditorTextFieldTypes.Day:
                text = RichTextFields.Day;
                break;

            case Core.Property.EditorTextFieldTypes.Month:
                text = RichTextFields.Month;
                break;

            case Core.Property.EditorTextFieldTypes.Year:
                text = RichTextFields.Year;
                break;

            case Core.Property.EditorTextFieldTypes.Criteria:
                text = RichTextFields.Criteria;
                break;

            case Core.Property.EditorTextFieldTypes.Category:
                text = RichTextFields.Category;
                break;

            case Core.Property.EditorTextFieldTypes.CategoryHeading:
                text = RichTextFields.CategoryHeading;
                break;

            case Core.Property.EditorTextFieldTypes.BusRoute:
                text = RichTextFields.BusRoute;
                getBusInfo = true;
                break;

            case Core.Property.EditorTextFieldTypes.BusRouteDescription:
                text = RichTextFields.BusRouteDescription;
                getBusInfo = true;
                break;

            case Core.Property.EditorTextFieldTypes.BusStop:
                text = RichTextFields.BusStop;
                getBusInfo = true;
                getBusStopInfo = true;
                break;

            case Core.Property.EditorTextFieldTypes.BusDay:
                text = RichTextFields.BusDay;
                getBusInfo = true;
                break;

            default:
                return Content(string.Empty);
        }

        IDictionary<string, string>? extra = null;
        if (getBusInfo && viewService.CurrentBusRouteData?.EndpointBusRoute != null && viewService.CurrentBusRouteData.EndpointBusStop != null)
        {
            var criteria = viewService.CurrentSearchCriteria(BusRoute.BusRouteViewComponent.QueryString)?.Split(',');
            if (criteria?.Any() == true && !string.IsNullOrWhiteSpace(criteria[0]))
            {
                var busInfo = busRouteService.GetBusService(new Uri(viewService.CurrentBusRouteData.EndpointBusRoute), criteria[0]);
                var attr = busInfo?.Features?.FirstOrDefault()?.Attribute;
                extra = new Dictionary<string, string>();
                if (attr != null && attr.Id != null && attr.Route != null)
                {
                    extra.Add(RichTextFields.BusRoute, attr.Id);
                    extra.Add(RichTextFields.BusRouteDescription, attr.Route);
                }

                if (getBusStopInfo && criteria.Length > 1 && !string.IsNullOrWhiteSpace(criteria[1]))
                {
                    var busStopInfo = busRouteService.GetBusStop(new Uri(viewService.CurrentBusRouteData.EndpointBusStop), criteria[1]);
                    var busStop = busStopInfo?.Features?.FirstOrDefault()?.Attribute?.Stop;
                    if (!string.IsNullOrWhiteSpace(busStop))
                    {
                        extra.Add(RichTextFields.BusStop, busStop);
                    }
                }

                if (criteria.Length > 2)
                {
                    if (!int.TryParse(criteria[2], out var day))
                    {
                        day = -1;
                    }
                    if (day == (int)DayOfWeek.Saturday || criteria[2].StartsWith("sa", StringComparison.InvariantCultureIgnoreCase))
                    {
                        extra.Add(RichTextFields.BusDay, nameof(DayOfWeek.Saturday));
                    }
                    else if (day == (int)DayOfWeek.Sunday || criteria[2].StartsWith("su", StringComparison.InvariantCultureIgnoreCase))
                    {
                        extra.Add(RichTextFields.BusDay, nameof(DayOfWeek.Sunday));
                    }
                    else
                    {
                        extra.Add(RichTextFields.BusDay, nameof(DayOfWeek.Monday) + " to " + nameof(DayOfWeek.Friday));
                    }
                }
            }
        }

        var parse = viewService.Parse(text, extra);
        if (string.IsNullOrEmpty(parse))
        {
            return Content(string.Empty);
        }

        return View(new TextBlockFieldViewModel
        {
            Text = new HtmlEncodedString(parse),
            Subtheme = settings.SubthemeTyped == Core.Property.EditorSubthemes.Inherit ? Subtheme() : settings.SubthemeTyped,
            Shade = settings.ThemeShadeTyped == Core.Property.EditorThemeShades.Inherit ? ThemeShade() : settings.ThemeShadeTyped,
            OverrideColor = settings.OverrideColor
        });
    }
}
