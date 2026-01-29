using System.Text;

namespace Protenacity.Cake.Web.Core.Extensions;

public static class StringExtensions
{
    //  https://www.dotnetperls.com/levenshtein
    // Is optimized for speed
    public static int FuzzyMatch(this string s, string t)
    {
        if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(t))
        {
            return 0;
        }

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // Initialize arrays.
        for (int i = 0; i <= n; d[i, 0] = i++) ;

        for (int j = 0; j <= m; d[0, j] = j++) ;

        // Begin looping.
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                // Compute cost.
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + ((t[j - 1] == s[i - 1]) ? 0 : 1));
            }
        }
        // Return cost.
        return d[n, m];
    }

    public static IEnumerable<string> Words(this string value) => value.WordsAndPositions().Select(x => x.Item1);

    public static IEnumerable<Tuple<string, int>> WordsAndPositions(this string value)
    {
        var start = -1;
        var end = 0;
        do
        {
            if (end == value.Length || !char.IsLetterOrDigit(value[end]))
            {
                if (start != -1)
                {
                    yield return new Tuple<string, int>(value.Substring(start, end - start), start);
                    start = -1;
                }
            }
            else if (start == -1 && char.IsLetterOrDigit(value[end]))
            {
                start = end;
            }
        }
        while (end++ != value.Length);
    }

    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        try
        {
            var emailAddress = new System.Net.Mail.MailAddress(email);
            return (string.IsNullOrEmpty(emailAddress.DisplayName));
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static string ToTitleCase(this string? str)
    {
        if (str == null)
        {
            return string.Empty;
        }

        var text = new StringBuilder();
        bool whitespace = true;
        foreach (var ch in str)
        {
            if (char.IsWhiteSpace(ch))
            {
                if (!whitespace)
                {
                    text.Append(ch);
                }
                whitespace = true;
            }
            else if (whitespace)
            {
                text.Append(char.ToUpperInvariant(ch));
                whitespace = false;
            }
            else
            {
                text.Append(char.ToLowerInvariant(ch));
            }
        }

        return text.ToString().Trim();
    }
}
