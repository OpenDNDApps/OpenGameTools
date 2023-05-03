using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - 
/// </summary>
public static class TransformExtensions
{
    public static void DestroyChildren(this Transform transform, Transform toIgnore = null)
    {
        List<Transform> anakin = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child == toIgnore)
                continue;
            anakin.Add(child);
        }

        foreach (Transform childToBeMurdered in anakin)
        {
            transform.gameObject.SafeDestroy(childToBeMurdered.gameObject);
        }
    }

    public static void DestroySiblings(this Transform transform)
    {
        if (transform.parent == null || transform.parent == transform)
        {
            Debug.LogWarning($"DestroySiblings does not work on root transforms.");
            return;
        }

        transform.parent.DestroyChildren(transform);
    }

    public static void FindAllChildrenWithTag(this Transform rootTransform, string findTag, ref List<Transform> listToFill)
    {
        listToFill ??= new List<Transform>();

        foreach (Transform child in rootTransform)
        {
            if (child.CompareTag(findTag))
            {
                listToFill.Add(child);
            }

            FindAllChildrenWithTag(child, findTag, ref listToFill);
        }
    }

    public static void FindAllChildren(this Transform rootTransform, ref List<Transform> listToFill)
    {
        listToFill ??= new List<Transform>();

        foreach (Transform child in rootTransform)
        {
            listToFill.Add(child);
            FindAllChildren(child, ref listToFill);
        }
    }
    
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