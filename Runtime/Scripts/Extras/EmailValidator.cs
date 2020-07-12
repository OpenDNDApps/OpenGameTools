using System;
using System.Globalization;
using System.Text.RegularExpressions;

    // http://msdn.microsoft.com/en-us/library/01escwtf%28v=vs.110%29.aspx

/// <summary>
/// Handles validating an email
/// </summary>
internal class EmailValidator
{
    /// <summary>
    /// True if the address is invalid
    /// </summary>
    private bool invalid;

    /// <summary>
    /// True if email is valid
    /// </summary>
    /// <param name="strIn"></param>
    /// <returns></returns>
    public bool IsValidEmail(string strIn)
    {
        invalid = false;
        if (String.IsNullOrEmpty(strIn))
            return false;

        // Use IdnMapping class to convert Unicode domain names. 
        strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);

        if (invalid)
            return false;

        // Return true if strIn is in valid e-mail format. 
        return Regex.IsMatch(strIn,
                             @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                             RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// convert Unicode domain names
    /// </summary>
    /// <param name="match"></param>
    /// <returns></returns>
    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalid = true;
        }
        return match.Groups[1].Value + domainName;
    }
}