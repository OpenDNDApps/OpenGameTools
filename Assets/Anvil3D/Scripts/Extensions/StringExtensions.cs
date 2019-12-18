using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - https://extensionmethod.net/csharp
/// - https://github.com/timothymugayi/StringExtensions
/// </summary>
/// 
public static class StringExtensions
{
	/// <summary>
	/// Truncates the string to a specified length and replace the truncated to a ...
	/// </summary>
	/// <param name="value">string that will be truncated</param>
	/// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
	/// <returns>truncated string</returns>
	public static string Truncate(this string value, int maxLength, string ellipsis = "...")
	{
		int lengthToTake = Math.Min(maxLength, value.Length);

		return (lengthToTake < value.Length) ?
			string.Format("{0}{1}", value.Substring(0, lengthToTake), ellipsis) :
			value.Substring(0, lengthToTake);
	}

	/// <summary>
	/// Emulation of PHPs ucfirst()
	/// </summary>
	/// <param name="value">A composite format string</param>
	/// <returns>A copy of format in which the format items have been replaced by the System.String</returns>
	/// <remarks>https://extensionmethod.net/csharp/string/ucfirst</remarks>
	public static string UcFirst(this string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return string.Empty;
		}

		char[] chars = value.ToCharArray();
		chars[0] = char.ToUpper(chars[0]);

		return new string(chars);
	}

	/// <summary>
	///     Read in a sequence of words from standard input and capitalize each
	///     one (make first letter uppercase; make rest lowercase).
	/// </summary>
	/// <param name="s">string</param>
	/// <returns>Word with capitalization</returns>
	public static string Capitalize(this string s)
	{
		if (s.Length == 0)
		{
			return s;
		}
		return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
	}

	/// <summary>
	/// Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).
	/// </summary>
	/// <param name="mText"></param>
	/// <returns></returns>
	public static string ToTitleCase(this string mText)
	{
		if (mText.IsNullOrEmpty())
			return mText;

		// get globalization info
		System.Globalization.CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
		System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;

		// convert to title case
		return textInfo.ToTitleCase(mText);
	}

	public static string ToUrlFriendly(this string text)
	{
		string value = text.Normalize(NormalizationForm.FormD).Trim();
		StringBuilder builder = new StringBuilder();

		foreach (char c in text.ToCharArray())
			if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				builder.Append(c);

		value = builder.ToString();

		byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);

		value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", "_");

		return value.ToLowerInvariant();
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
	/// <param name="value"></param>
	/// <returns></returns>
	public static bool IsValidUrl(this string text)
	{
		Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
		return rx.IsMatch(text);
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
            DateTime tempDate;
            return DateTime.TryParse(value, out tempDate);
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
			return int.TryParse(value, out int tempInt);
		}
        catch (Exception)
        {
            return false;
        }
    }

	/// <summary>
	/// like LINQ skip - skips the first x characters and returns the remaining string
	/// </summary>
	/// <param name="value">the string</param>
	/// <param name="count">number of characters to skip</param>
	/// <returns></returns>
	public static string Skip(this string value, int count)
    {
        return value.Substring(Math.Min(count, value.Length) - 1);
    }

	/// <summary>
	/// Reverses the string
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string Reverse(this string input)
    {
        char[] chars = input.ToCharArray();
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
    /// <param name="value"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static bool Match(this string value, string pattern)
    {
        return Regex.IsMatch(value, pattern);
    }

    /// <summary>
    /// Returns the number of words in the given string.
    /// </summary>
    /// <param name="s">The given string.</param>
    /// <returns>The word count.</returns>
    public static int GetWordCount(this string s)
    {
        return (new Regex(@"\w+")).Matches(s).Count;
    }

    /// <summary>
    /// Checks whether the given string is a palindrome.
    /// </summary>
    /// <param name="s">The given string.</param>
    /// <returns>True if the given string is palindrome, false otherwise.</returns>
    public static bool IsPalindrome(this string s)
    {
        return s.Equals(s.Reverse());
    }

    /// <summary>
    /// Returns true if the email address is valid
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string email)
    {
        // fail if null or too long
        if ((string.IsNullOrEmpty(email)) || (email.Length > 100))
            return false;

        //// set to ignore case
        //Regex regex = new Regex(STR_EmailPattern, RegexOptions.IgnoreCase);
        //// return if the regex matches
        //return regex.IsMatch(email);

        // use the MSDN email validator
        return new EmailValidator().IsValidEmail(email);
    }

    /// <summary>
    /// Returns whether the given string is a valid IP address v4
    /// </summary>
    /// <param name="s">The given string.</param>
    /// <returns></returns>
    public static bool IsValidIPv4(this string s)
    {
		System.Net.IPAddress.TryParse(s, out System.Net.IPAddress address);
		return (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
	}

	/// <summary>
	/// Returns whether the given string is a valid IP address v6
	/// </summary>
	/// <param name="s">The given string.</param>
	/// <returns></returns>
	/// <summary>
	/// Returns whether the given string is a valid IP address v4 or v6
	/// </summary>
	/// <param name="s">The given string.</param>
	public static bool IsValidIPv6(this string s)
	{
		System.Net.IPAddress.TryParse(s, out System.Net.IPAddress address);
		return (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);
	}

	/// <returns></returns>
	public static bool IsValidIP(this string s)
	{
		return s.IsValidIPv4() || s.IsValidIPv6();
	}
}
