using System.ComponentModel;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class Enum<T> where T : Enum
{
    public static T GetValueByDescription(string? description)
    {
        var fields = typeof(T).GetFields();
        if (fields?.Any() != true)
        {
            throw new ApplicationException(nameof(T) + " doesn't haved any values");
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            foreach (var field in fields.Skip(1))
            {
                if ((Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
                    && string.Compare(attribute.Description, description, true) == 0) || string.Compare(field.Name, description) == 0)
                {
                    return (T)(field.GetValue(null) ?? throw new ApplicationException("Invalid situation"));
                }
            }
        }
        return (T) (fields[1]?.GetValue(null) ?? throw new ApplicationException(nameof(T) + " doesn't haved any values"));
    }

    public static T GetValueByDescription(IEnumerable<string>? descriptions, T defaultValue)
    {
        var fields = typeof(T).GetFields();
        if (fields?.Any() != true)
        {
            throw new ApplicationException(nameof(T) + " doesn't haved any values");
        }

        if (descriptions?.Any() != true)
        {
            return defaultValue;
        }

        int value = 0;

        foreach (var description in descriptions)
        {
            foreach (var field in fields.Skip(1))
            {
                if ((Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
                    && string.Compare(attribute.Description, description, true) == 0) || string.Compare(field.Name, description) == 0)
                {
                    value = value | (int)(field.GetValue(null) ?? throw new ApplicationException("Invalid situation"));
                }
            }
        }
        return (T)(object) value;
    }


    public static string GetDescriptionByValue(T value)
    {
        if (value == null)
        {
            var fields = typeof(T).GetFields();
            value = (T)(fields[1]?.GetValue(null) ?? throw new ApplicationException(nameof(T) + " doesn't haved any values"));
        }
        var attribute = value.GetType().GetField(value.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
