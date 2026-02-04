using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class EnumExtensions
{

    private static T FirstValue<T>()
        => (T)((typeof(T).GetFields())[1]?.GetValue(null) ?? throw new ApplicationException(nameof(T) + " doesn't haved any values"));

    private static T Find<T>(string? description, bool ignoreCase, T defaultValue)
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

    private static T Find<T>(IEnumerable<string>? descriptions, bool ignoreCase, T defaultValue)
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

        public static T ParseByDescription(string? description)
            => Find<T>(description, true, FirstValue<T>());

        public static T ParseByDescription(string? description, bool ignoreCase)
            => Find<T>(description, ignoreCase, FirstValue<T>());

        public static T ParseByDescription(string? description, T defaultValue)
            => Find<T>(description, true, defaultValue);

        public static T ParseByDescription(string? description, bool ignoreCase, T defaultValue)
            => Find<T>(description, ignoreCase, defaultValue);

        public static T ParseByDescription(IEnumerable<string>? descriptions)
            => Find<T>(descriptions, true, FirstValue<T>());

        public static T ParseByDescription(IEnumerable<string>? descriptions, bool ignoreCase)
            => Find<T>(descriptions, ignoreCase, FirstValue<T>());

        public static T ParseByDescription(IEnumerable<string>? descriptions, bool ignoreCase, T defaultValue)
            => Find<T>(descriptions, ignoreCase, defaultValue);

        public static T ParseByDescription(IEnumerable<string>? descriptions, T defaultValue)
            => Find<T>(descriptions, true, defaultValue);

        public string Description
        {
            get
            {
                var attribute = typeof(T).GetField(source.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                return attribute == null ? source.ToString() : attribute.Description;
            }
        }
    }
}

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
