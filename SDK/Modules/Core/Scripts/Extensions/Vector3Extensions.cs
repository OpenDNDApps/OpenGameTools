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
    
    public static Vector3 SnapToGrid(this Vector3 position, float gridSpacing)
    {
        return new Vector3(
            Mathf.Round(position.x / gridSpacing) * gridSpacing,
            Mathf.Round(position.y / gridSpacing) * gridSpacing,
            Mathf.Round(position.z / gridSpacing) * gridSpacing
        );
    }

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

    public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
    {
        return a + (b - a) * t;
    }

    public static Vector2 RoundToInt(this Vector2 v)
    {
        return new Vector2(
            Mathf.RoundToInt(v.x),
            Mathf.RoundToInt(v.y)
        );
    }

    public static Vector3 RandomPointInBounds(this Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static Vector3 ProjectOntoPlane(this Vector3 v, Vector3 normal)
    {
        return v - Vector3.Dot(v, normal) * normal;
    }
    
    public static Vector3 SmoothDamp(this Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
    {
        float deltaTime = Time.deltaTime;
        return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, Mathf.Infinity, deltaTime);
    }
    
    public static Vector2 Scale(this Vector2 v, Vector2 scale)
    {
        return new Vector2(v.x * scale.x, v.y * scale.y);
    }

    public static Vector3 NearestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 lineDirection = lineEnd - lineStart;
        float t = Vector3.Dot(point - lineStart, lineDirection) / lineDirection.sqrMagnitude;
        return lineStart + Mathf.Clamp01(t) * lineDirection;
    }
    
    public static Vector3 RotateAround(this Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }
    
    public static Vector3 Perpendicular(this Vector3 v)
    {
        if (Mathf.Abs(v.x) < Mathf.Abs(v.z))
        {
            return new Vector3(0f, -v.z, v.y);
        }

        return new Vector3(-v.y, v.x, 0f);
    }
    
    public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
    {
        float angle = Vector3.Angle(a, b);
        if (angle == 0f)
        {
            return a;
        }

        Vector3 axis = Vector3.Cross(a, b).normalized;
        Quaternion rotation = Quaternion.AngleAxis(angle * t, axis);
        return rotation * a;
    }
}