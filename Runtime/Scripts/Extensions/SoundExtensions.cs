using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - 
/// </summary>
/// 
public static class SoundExtensions
{
	/// <summary>
	///  Sets the value of the exposed parameter specified as a normalized percent value, 0-1. When a parameter is exposed,
	///  it is not controlled by mixer snapshots and can therefore only be changed via this function.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="value">New value of exposed parameter.</param>
	public static bool SetVolume(this AudioMixer mixer, string name, float value)
	{
		value = Mathf.Clamp(value, 0.001f, 1f);
		value = Mathf.Log(value) * 20f;

		return mixer.SetFloat(name, value);
	}
}
