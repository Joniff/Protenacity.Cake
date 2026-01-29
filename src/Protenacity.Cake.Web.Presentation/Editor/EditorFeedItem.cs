using Sagara.FeedReader;
using Sagara.FeedReader.Feeds;

namespace Protenacity.Cake.Web.Presentation.Editor;

public class EditorFeedItem : FeedItem
{
    private string? imageUrl;

    public string? ImageUrl
    {
        get
        {
            switch(SpecificItem)
            {
                case MediaRssFeedItem media:
                    var thumbnails = media.Media.SelectMany(m => m.Thumbnails).Where(t => !string.IsNullOrWhiteSpace(t.Url));

                    if (thumbnails.Any())
                    {
                        return thumbnails.First().Url;
                    }

                    if (!string.IsNullOrWhiteSpace(media.Enclosure?.Url))
                    {
                        return media.Enclosure?.Url;
                    }

                    return media.ItemOrEntryElement?.Elements().FirstOrDefault(n => string.Compare(n.Name.LocalName, "thumbnail", true) == 0)
                        ?.Attributes().FirstOrDefault(a => string.Compare(a.Name.LocalName, "url", true) == 0)?.Value ?? imageUrl;

                case Rss20FeedItem rss20:
                    if (!string.IsNullOrWhiteSpace(rss20.Enclosure?.Url))
                    {
                        return rss20.Enclosure?.Url;
                    }
                    return imageUrl;

                case Rss092FeedItem rss092:
                    if (!string.IsNullOrWhiteSpace(rss092.Enclosure?.Url))
                    {
                        return rss092.Enclosure?.Url;
                    }
                    return imageUrl;

                default:
                    return imageUrl;
            }
        }
        set
        {
            imageUrl = value;
        }
    }
}
