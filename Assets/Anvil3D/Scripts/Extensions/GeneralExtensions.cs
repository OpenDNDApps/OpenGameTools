using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - 
/// </summary>
/// 
public static class GeneralExtensions
{

	/// <summary>
	/// Checks if the flags contains the desiredFlag, like ((129&128) == 128)
	/// </summary>
	/// <typeparam name="T">Must be an Enum</typeparam>
	/// <param name="flags">Total flags in object</param>
	/// <param name="desiredFlag">Desired flag</param>
	/// <returns>returns true if the desiredFlag is in flags</returns>
	public static bool HasFlag<T>(T flags, T desiredFlag)
	{
		if (!flags.GetType().IsEnum || !desiredFlag.GetType().IsEnum)
			throw new NotImplementedException();

		return ((int)(object)flags & (int)(object)desiredFlag) == (int)(object)desiredFlag;
	}

	/// <summary>
	/// Checks if the flags contains the desiredFlag, like ((129&128) == 128)
	/// </summary>
	/// <typeparam name="T">Must be an Enum</typeparam>
	/// <param name="flags">Total flags in object</param>
	/// <param name="desiredFlag">Desired flag</param>
	/// <returns>returns true if the desiredFlag is in flags</returns>
	public static bool HasFlag<T>(this MonoBehaviour _, T _toCheck, T desiredFlag) { return HasFlag(_toCheck, desiredFlag); }

	/// <summary>
	/// Checks if the flags contains the desiredFlag, like ((129&128) == 128)
	/// </summary>
	/// <typeparam name="T">Must be an Enum</typeparam>
	/// <param name="flags">Total flags in object</param>
	/// <param name="desiredFlag">Desired flag</param>
	/// <returns>returns true if the desiredFlag is in flags</returns>
	public static bool HasFlag<T>(this ScriptableObject _, T flags, T desiredFlag) { return HasFlag(flags, desiredFlag); }
}
