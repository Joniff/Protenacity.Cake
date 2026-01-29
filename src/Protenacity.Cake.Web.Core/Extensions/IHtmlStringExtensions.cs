using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class IHtmlContentExtensions
{
    public static string Truncate(this IHtmlEncodedString htmlString, int maxLength, string suffix = "...") => String.IsNullOrWhiteSpace(htmlString.ToString()) ? "" : htmlString.ToText().Truncate(maxLength, suffix);

    private static IHtmlEncodedString EmptyHtmlString() => new HtmlEncodedString("");

    public static IHtmlEncodedString RemoveEnclosingParagraph(this IHtmlEncodedString data)
    {
        if (data == null)
        {
            return EmptyHtmlString();
        }

        var text = data.ToHtmlString()?.Trim() ?? "";

        if (text.StartsWith("<p>") && text.EndsWith("</p>"))
        {
            return new HtmlEncodedString(text.Substring(3, text.Length - 7));
        }

        return data;
    }

    public static IHtmlEncodedString ToHeader(this IHtmlEncodedString data)
    {
        if (data == null)
        {
            return EmptyHtmlString();
        }

        var document = new HtmlDocument();
        document.LoadHtml(data.ToString()!);

        var found = document.DocumentNode.SelectNodes("./*|./text()");
        if (found?.Any() != true)
        {
            return EmptyHtmlString();
        }

        var acceptableTags = new String[] { "strong", "em", "u", "sup", "sub" };
        var nodes = new Queue<HtmlNode>(found);
        while (nodes.Any())
        {
            var node = nodes.Dequeue();
            var parentNode = node.ParentNode;

            if (!acceptableTags.Contains(node.Name) && node.Name != "#text")
            {
                var childNodes = node.SelectNodes("./*|./text()");

                if (childNodes != null)
                {
                    foreach (var child in childNodes)
                    {
                        nodes.Enqueue(child);
                        parentNode.InsertBefore(child, node);
                    }
                }

                parentNode.RemoveChild(node);

            }
        }
        return new HtmlEncodedString(document.DocumentNode.InnerHtml);
    }

    public static IHtmlEncodedString ToText(this IHtmlEncodedString data)
    {
        if (data == null)
        {
            return EmptyHtmlString();
        }

        var document = new HtmlDocument();
        document.LoadHtml(data.ToString()!);

        var found = document.DocumentNode.SelectNodes("./*|./text()");
        if (found?.Any() != true)
        {
            return EmptyHtmlString();
        }

        var nodes = new Queue<HtmlNode>(found);
        while (nodes.Any())
        {
            var node = nodes.Dequeue();
            var parentNode = node.ParentNode;

            if (node.Name != "#text")
            {
                var childNodes = node.SelectNodes("./*|./text()");

                if (childNodes != null)
                {
                    foreach (var child in childNodes)
                    {
                        nodes.Enqueue(child);
                        parentNode.InsertBefore(child, node);
                    }
                }

                parentNode.RemoveChild(node);
            }
        }
        return new HtmlEncodedString(document.DocumentNode.InnerHtml);
    }

    public static bool HasContent(this IHtmlEncodedString data)
    {
        if (data == null)
        {
            return false;
        }

        var document = new HtmlDocument();
        document.LoadHtml(data.ToString()!);
        var found = document.DocumentNode.SelectNodes("./*|./text()");
        return (found != null && found.Any());
    }
}
