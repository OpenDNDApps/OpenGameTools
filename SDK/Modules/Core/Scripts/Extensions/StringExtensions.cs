using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - https://extensionmethod.net/csharp
/// - https://github.com/timothymugayi/StringExtensions
/// </summary>

public static class StringExtensions
{
    /// <summary>
    /// Truncates the string to a specified length and replace the truncated to a ...
    /// </summary>
    /// <param name="str">string that will be truncated</param>
    /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
    /// <param name="ellipsis">...</param>
    /// <returns>truncated string</returns>
    public static string Truncate(this string str, int maxLength, string ellipsis = "...")
    {
        int lengthToTake = Math.Min(maxLength, str.Length);
        return (lengthToTake < str.Length) ? $"{str[..lengthToTake]}{ellipsis}" : str[..lengthToTake];
    }
    
    public static string ToLowerFirst(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        char firstChar = char.ToLower(str[0]);
        return firstChar + str[1..];
    }

    /// <summary>
    /// Emulation of PHPs UcFirst()
    /// </summary>
    /// <param name="str">A composite format string</param>
    /// <returns>A copy of format in which the format items have been replaced by the System.String</returns>
    /// <remarks>https://extensionmethod.net/csharp/string/ucfirst</remarks>
    public static string UcFirst(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        char[] chars = str.ToCharArray();
        chars[0] = char.ToUpper(chars[0]);

        return new string(chars);
    }

    /// <summary>
    /// Read in a sequence of words from standard input and capitalize each
    /// one (make first letter uppercase; make rest lowercase).
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>Word with capitalization</returns>
    public static string Capitalize(this string str)
    {
        if (str.Length == 0)
        {
            return str;
        }
        return str[..1].ToUpper() + str[1..].ToLower();
    }

    /// <summary>
    /// Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToTitleCase(this string str)
    {
        if (str.IsNullOrEmpty())
            return str;

        CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;

        return textInfo.ToTitleCase(str);
    }

    public static string ToUrlFriendly(this string str)
    {
        string value = str.Normalize(NormalizationForm.FormD).Trim();
        StringBuilder builder = new StringBuilder(value);
        
        foreach (char c in str)
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        
        value = builder.ToString();
        
        byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(str);
        
        value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "_");
        
        return value.ToLowerInvariant();
    }

    public static string AsSlug(this string str)
    {
        string value = str.Normalize(NormalizationForm.FormD).Trim();
        StringBuilder builder = new StringBuilder(value);
        
        foreach (char c in str.ToCharArray())
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        
        value = builder.ToString();
        
        byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(str);
        
        value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "-");
        
        return value.ToLowerInvariant();
    }

    public static string AsMD5Hash(this string text)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    ///  Replaces the format item in a specified System.String with the text equivalent
    ///  of the value of a specified System.Object instance.
    /// </summary>
    /// <param name="value">A composite format string</param>
    /// <param name="args">An System.Object array containing zero or more objects to format.</param>
    /// <returns>A copy of format in which the format items have been replaced by the System.String
    /// equivalent of the corresponding instances of System.Object in args.</returns>
    public static string Format(this string value, params object[] args)
    {
        return string.Format(value, args);
    }

    /// <summary>
    /// Null or empty check as extension
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Returns true if the string is a valid http or https url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsValidUrl(this string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    public static bool IsValidImageURL(this string url)
    {
        if (!url.IsValidUrl())
            return false;
        
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        string extension = System.IO.Path.GetExtension(url);
        return imageExtensions.Contains(extension.ToLower());
    }

    public static bool IsValidVideoUrl(this string url)
    {
        if (!url.IsValidUrl())
            return false;

        string[] videoExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".webm" };
        string extension = System.IO.Path.GetExtension(url);

        return videoExtensions.Contains(extension.ToLower());
    }

    /// <summary>
    /// Returns true if value is a date
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDate(this string value)
    {
        try
        {
            return DateTime.TryParse(value, out _);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Returns true if value is an int
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsInt(this string value)
    {
        try
        {
            return int.TryParse(value, out _);
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Reverses the string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Reverse(this string str)
    {
        char[] chars = str.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    /// <summary>
    /// ditches html tags - note it doesn't get rid of things like nbsp;
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string StripHtml(this string html)
    {
        if (html.IsNullOrEmpty())
            return string.Empty;

        return Regex.Replace(html, @"<[^>]*>", string.Empty);
    }

    /// <summary>
    /// Returns true if the pattern matches
    /// </summary>
    /// <param name="str"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static bool Match(this string str, string pattern)
    {
        return Regex.IsMatch(str, pattern);
    }

    /// <summary>
    /// Returns the number of words in the given string.
    /// </summary>
    /// <param name="str">The given string.</param>
    /// <returns>The word count.</returns>
    public static int GetWordCount(this string str)
    {
        return new Regex(@"\w+").Matches(str).Count;
    }

    /// <summary>
    /// Returns true if the email address is valid
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    /// <summary>
    /// Returns whether the given string is a valid IP address v4
    /// </summary>
    /// <param name="str">The given string.</param>
    /// <returns></returns>
    public static bool IsValidIPv4(this string str)
    {
        System.Net.IPAddress.TryParse(str, out System.Net.IPAddress address);
        return (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
    }

    /// <summary>
    /// Returns whether the given string is a valid IP address v6
    /// </summary>
    /// <param name="str">The given string.</param>
    /// <returns></returns>
    public static bool IsValidIPv6(this string str)
    {
        System.Net.IPAddress.TryParse(str, out System.Net.IPAddress address);
        return (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
    }

    /// <returns></returns>
    public static bool IsValidIP(this string str)
    {
        return str.IsValidIPv4() || str.IsValidIPv6();
    }

    public static int AsInt(this string str)
    {
        return int.Parse(str);
    }
}