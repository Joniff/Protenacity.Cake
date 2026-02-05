using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class EnumExtensions
{
    public static T? ParseByDescription<T>(string? description, bool ignoreCase, T? defaultValue)
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
                    && string.Compare(attribute.Description, description, ignoreCase) == 0) || string.Compare(field.Name, description, ignoreCase) == 0)
                {
                    return (T)(field.GetValue(null) ?? throw new ApplicationException("Invalid situation"));
                }
            }
        }

        return defaultValue;
    }

    public static T? ParseByDescription<T>(IEnumerable<string>? descriptions, bool ignoreCase, T? defaultValue)
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
                    && string.Compare(attribute.Description, description, ignoreCase) == 0) || string.Compare(field.Name, description, ignoreCase) == 0)
                {
                    value = value | (int)(field.GetValue(null) ?? throw new ApplicationException("Invalid situation"));
                }
            }
        }

        return value == 0 ? defaultValue : (T)(object)value;
    }

    public static string? Description<T>(T source)
        => (typeof(T).GetField(source?.ToString() ?? throw new ArgumentNullException())?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute)?.Description;
}


public static class EnumExtensions14
{
    extension<T>(T source) where T : struct, Enum
    {
        public static T Parse(string value)
            => Enum.Parse<T>(value);

        public static T Parse(string value, bool ignoreCase)
            => Enum.Parse<T>(value, ignoreCase);

        public static T Parse(ReadOnlySpan<char> value)
            => Enum.Parse<T>(value);

        public static T Parse(ReadOnlySpan<char> value, bool ignoreCase)
            => Enum.Parse<T>(value, ignoreCase);

        public static bool TryParse([NotNullWhen(true)] string? value, out T result)
            => Enum.TryParse(value, out result);

        public static bool TryParse([NotNullWhen(true)] string? value, bool ignoreCase, out T result)
            => Enum.TryParse(value, ignoreCase, out result);

        public static bool TryParse(ReadOnlySpan<char> value, out T result)
            => Enum.TryParse(value, out result);

        public static bool TryParse(ReadOnlySpan<char> value, bool ignoreCase, out T result)
            => Enum.TryParse(value, ignoreCase, out result);

        public static T? ParseByDescription(string? description)
            => EnumExtensions.ParseByDescription<T>(description, true, default(T));

        public static T? ParseByDescription(string? description, bool ignoreCase)
            => EnumExtensions.ParseByDescription<T>(description, ignoreCase, default(T));

        public static T? ParseByDescription(string? description, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(description, true, defaultValue);

        public static T? ParseByDescription(string? description, bool ignoreCase, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(description, ignoreCase, defaultValue);

        public static T? ParseByDescription(IEnumerable<string>? descriptions)
            => EnumExtensions.ParseByDescription<T>(descriptions, true, default(T));

        public static T? ParseByDescription(IEnumerable<string>? descriptions, bool ignoreCase)
            => EnumExtensions.ParseByDescription<T>(descriptions, ignoreCase, default(T));

        public static T? ParseByDescription(IEnumerable<string>? descriptions, bool ignoreCase, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(descriptions, ignoreCase, defaultValue);

        public static T? ParseByDescription(IEnumerable<string>? descriptions, T defaultValue)
            => EnumExtensions.ParseByDescription<T>(descriptions, true, defaultValue);

        public string? Description => EnumExtensions.Description<T>(source);
    }
}

