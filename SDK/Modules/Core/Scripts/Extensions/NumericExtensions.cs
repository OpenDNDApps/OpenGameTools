using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - 
/// </summary>
/// Should I put these on StringExtensions? I don't know...
/// 
namespace OGT
{
	public static class NumericExtensions
	{
		/// <summary>
		/// returns true ONLY if the entire string is numeric
		/// </summary>
		/// <param name="input">the string to test</param>
		public static bool IsNumeric(this string input)
		{
			return (!string.IsNullOrEmpty(input)) && (new Regex(@"^-?[0-9]*\.?[0-9]+$").IsMatch(input.Trim()));
		}

		/// <summary>
		/// returns true if any part of the string is numeric
		/// </summary>
		/// <param name="input">the string to test</param>
		public static bool HasNumeric(this string input)
		{
			return (!string.IsNullOrEmpty(input)) && (new Regex(@"[0-9]+").IsMatch(input));
		}
	}
}