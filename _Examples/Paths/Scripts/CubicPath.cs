namespace Anvil3D
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

	[Serializable]
	public class CubicPath
	{
		public List<Transform> waypoints;

		/// <summary>
		/// 
		/// More info in https://en.wikipedia.org/wiki/B%C3%A9zier_curve
		/// </summary>
		/// <param name="t"></param>
		public Vector3 GetPoint(float t)
		{
			if (waypoints.Count != 4)
			{
				Debug.LogError($"Cubic Paths need 4 waypoints.");
				return Vector3.zero;
			}
			if (t < 0f || t > 1f)
			{
				Debug.LogError($"Cubic Paths values are clamped 0-1");
				t = Mathf.Clamp01(t);
			}

			float u = 1 - t;
			float tt = t * t;
			float uu = u * u;
			float uuu = u * u * u;
			float ttt = t * t * t;
			Vector3 point = uuu * waypoints[0].position;
			point += 3 * uu * t * waypoints[1].position;
			point += 3 * u * tt * waypoints[2].position;
			point += ttt * waypoints[3].position;
			return point;
		}
	}
}