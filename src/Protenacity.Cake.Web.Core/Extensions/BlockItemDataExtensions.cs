using Umbraco.Cms.Core.Models.Blocks;
using static System.Net.Mime.MediaTypeNames;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class BlockItemDataExtensions
{
    private static bool Char2Bool(char ch)
    {
        switch (ch)
        {
            case 'T':
            case 't':
            case '1':
                return true;

            default:
                return false;
        }
    }

    public static T? Value<T>(this BlockItemData blockItemData, string alias)
    {
        var obj = blockItemData?.Values?.FirstOrDefault(v => v.Alias == alias)?.Value;
        if (obj == null)
        {
            return default(T?);
        }

        if (typeof(T) == typeof(string) || typeof(T) == typeof(int))
        {
            return (T?)obj;
        }
        if (typeof(T) == typeof(bool))
        {
            if (obj.GetType() == typeof(string))
            {
                var text = (string)obj;
                return (T?)(object)(text.Length == 0 ? false : Char2Bool(text[0]));
            }
            if (obj.GetType() == typeof(char))
            {
                return (T?)(object)Char2Bool((char)obj);
            }
            if (obj.GetType() == typeof(int))
            {
                return (T?)(object)((int)obj != 0 ? true : false);
            }
            if (obj.GetType() == typeof(bool))
            {
                return (T?)(object)((bool)obj);
            }
        }
        throw new ArgumentOutOfRangeException("No BlockItemData.Value extension converter for " + obj.GetType().Name);
    }
}
