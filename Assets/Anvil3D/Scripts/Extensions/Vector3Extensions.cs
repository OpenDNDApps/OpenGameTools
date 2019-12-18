using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - https://github.com/mminer/unity-extensions
/// </summary>
public static class Vector3Extensions
{
	/// <summary>
	/// Returns the Vector3 distance to the target
	/// </summary>
	/// <param name="origin"></param>
	/// <param name="target"></param>
	/// <returns></returns>
	public static float DistanceTo(this Vector3 origin, Vector3 target)
	{
		return Vector3.Distance(origin, target);
	}

	/// <summary>
	/// Finds the position closest to the given one.
	/// </summary>
	/// <param name="position">World position.</param>
	/// <param name="otherPositions">Other world positions.</param>
	/// <returns>Closest position.</returns>
	public static Vector3 GetClosestFromList(this Vector3 position, IEnumerable<Vector3> otherPositions)
	{
		Vector3 closest = Vector3.zero;
		float shortestDistance = Mathf.Infinity;

		foreach (Vector3 otherPosition in otherPositions)
		{
			float distance = (position - otherPosition).sqrMagnitude;

			if (distance < shortestDistance)
			{
				closest = otherPosition;
				shortestDistance = distance;
			}
		}

		return closest;
	}
}