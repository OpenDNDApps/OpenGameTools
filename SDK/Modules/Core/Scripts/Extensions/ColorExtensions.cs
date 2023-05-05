using UnityEngine;

/// <summary>
/// This collection of extensions contain code from:
///		- 
///		
/// Extension methods for UnityEngine.Component.
/// </summary>
/// 
public static class ColorExtensions
{
	/// <summary>
	/// Converts a HEX to Color, default or error is magenta
	/// </summary>
	/// <param name="hex">An RGB or an ARGB string</param>
	/// <returns>Color object</returns>
	public static Color ToColor(this string hex)
	{
		bool valid = ColorUtility.TryParseHtmlString(hex, out Color color);
		return valid ? color : Color.magenta;
	}

	/// <summary>
	/// Returns a new color with the specified alpha value
	/// </summary>
	/// <param name="color"></param>
	/// <param name="alpha"></param>
	/// <returns></returns>
	public static Color WithAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}
	
	/// <summary>
	/// Returns AABBCCDD
	/// </summary>
	/// <param name="color"></param>
	/// <returns></returns>
	public static string ToHexString(this Color color)
	{
		return
			((byte)(color.r * 255)).ToString("X2") +
			((byte)(color.g * 255)).ToString("X2") +
			((byte)(color.b * 255)).ToString("X2") +
			((byte)(color.a * 255)).ToString("X2");
	}
    
	/// <summary>
	/// Returns #AABBCCDD
	/// </summary>
	/// <param name="color"></param>
	/// <returns></returns>
	public static string ToHashHexString(this Color color)
	{
		return $"#{color.ToHexString()}";
	}
}
