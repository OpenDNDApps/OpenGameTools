using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace OGT
{
	public static class NumericExtensions
	{
		public static bool AlmostEquals(this float value, float target, float precision = float.Epsilon)
		{
			return Mathf.Abs(value - target) <= precision;
		}
		
		public static bool AlmostEquals(this double value, double target, double precision = double.Epsilon)
		{
			return Math.Abs(value - target) <= precision;
		}
		
		/// <summary>
		/// returns true ONLY if the entire string is numeric
		/// </summary>
		/// <param name="input">target string</param>
		public static bool IsNumeric(this string input)
		{
			return (!string.IsNullOrEmpty(input)) && (new Regex(@"^-?[0-9]*\.?[0-9]+$").IsMatch(input.Trim()));
		}

		/// <summary>
		/// returns true IF ANY part of the string is numeric
		/// </summary>
		/// <param name="input">target string</param>
		public static bool HasNumeric(this string input)
		{
			return (!string.IsNullOrEmpty(input)) && (new Regex(@"[0-9]+").IsMatch(input));
		}
	}
}