using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Property;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.CodeAnalysis;
using System;
using System.Globalization;

namespace Protenacity.Cake.Web.Presentation.Editor.Map;

public class MapViewComponent : ViewComponent
{
    public const string Name = nameof(Map);
    public const string Template = "~/Views/Components/" + Name + "/Default.cshtml";

    public IViewComponentResult Invoke(IEditorContent content)
    {
        var map = content.Block?.Content as IEditorMapBase;
        var settings = content.Block?.Settings as IEditorMapBaseSettings;

        if (map?.MapLocation?.Marker == null || settings == null)
        {
            return Content("");
        }

        var icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/0525c29460954a70ae609e4853cbc504/data";

        switch (settings.IconTyped)
        {
            case EditorMapIcons.BluePin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/3ce0704db67d4454930e20d8c197741e/data";
                break;

            case EditorMapIcons.GreenPin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/27b8ce2f277949c68d14ad7be5e7d3ad/data";
                break;

            case EditorMapIcons.GreyPin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/0117ee38b7e44cf08e5da53bd7ac1b1c/data";
                break;

            case EditorMapIcons.OrangePin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/96aab3dfb2ac4d31a8dbd6273a0bc17c/data";
                break;

            case EditorMapIcons.PurpleCircle:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/28b370d7e8164027bb611de64ba83a86/data";
                break;

            case EditorMapIcons.PurplePin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/0525c29460954a70ae609e4853cbc504/data";
                break;

            case EditorMapIcons.PurplePush1:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/bb93d02036874ed087f9d11eb868f72a/data";
                break;

            case EditorMapIcons.PurplePush2:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/233ce38f3917439daf0ac9ac89e7b53f/data";
                break;

            case EditorMapIcons.PurpleTab:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/5cb9b1c99ac64550bbabb16a5f563293/data";
                break;

            case EditorMapIcons.PurpleX:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/d95e985863a14163aeef26c9459e52a8/data";
                break;

            case EditorMapIcons.RedPin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/ad09c581745b41a3bbc11dff91f7876a/data";
                break;

            case EditorMapIcons.YellowPin:
                icon = "https://lancashirecounty.maps.arcgis.com/sharing/rest/content/items/0097948cf65d4446ab16259984ecaab2/data";
                break;
        }

        return View(new MapViewModel
        {
            MapType = map.MapTypeTyped,
            Id = Name + Guid.NewGuid().ToString("N"),
            Latitude = map.MapLocation.Marker.Latitude,
            Longitude = map.MapLocation.Marker.Longitude,
            Name = map.MapName ?? string.Empty,
            IconUrl = icon,
            Zoom = map.MapLocation.Zoom,
            Ratio = settings.RatioCalculated
        });
    }
}
