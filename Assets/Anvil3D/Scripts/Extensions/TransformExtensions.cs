using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - 
/// </summary>
public static class TransformExtensions
{
	/// <summary>
	/// Finds the position closest to the given one.
	/// </summary>
	/// <param name="transform">World position.</param>
	/// <param name="otherTransforms">Other world positions.</param>
	/// <returns>Closest transform.</returns>
	public static Vector3 GetClosestFromList(this Transform transform, IEnumerable<Transform> otherTransforms)
	{
		Vector3 closest = Vector3.zero;
		float shortestDistance = Mathf.Infinity;

		foreach (Transform otherTransform in otherTransforms)
		{
			float distance = (transform.position - otherTransform.position).sqrMagnitude;

			if (distance < shortestDistance)
			{
				closest = otherTransform.position;
				shortestDistance = distance;
			}
		}

		return closest;
	}
}