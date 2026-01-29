using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class BlockItemDataExtensions
{
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
            return (T?)(object)(((int?)obj) != 0);
        }
        throw new ArgumentOutOfRangeException("No BlockItemData.Value extension converter for " + obj.GetType().Name);
    }
}
