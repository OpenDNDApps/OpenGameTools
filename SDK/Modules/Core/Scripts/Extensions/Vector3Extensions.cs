using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// For example (1,2,3) this returns Vector3(1,0,2)
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 X0Y(this Vector2 vector)
    {
        return new Vector3(vector.x, 0f, vector.y);
    }

    /// <summary>
    /// For example (1,2,3) this returns Vector2(1,2)
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector2 XY(this Vector3 vector)
    {
        return vector;
    }

    /// <summary>
    /// For example (1,2,3) this returns Vector2(1,3)
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector2 XZ(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    /// <summary>
    /// For example (1,2,3) this returns Vector2(3,1)
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector2 ZX(this Vector3 vector)
    {
        return new Vector2(vector.z, vector.x);
    }
    
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