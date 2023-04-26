using UnityEngine;

/// <summary>
/// Some of this code is from:
/// - https://github.com/mminer/unity-extensions
/// </summary>

public static class GameObjectExtensions
{
    public static void SafeDestroy(this GameObject go, GameObject target)
    {
        if (target == null) return;
        target.SetActive(false);
        Object.Destroy(target, 0.001f);
    }
    
    /// <summary>
    /// Gets a component attached to the given game object.
    /// If one isn't found, a new one is attached and returned.
    /// </summary>
    /// <param name="gameObject">Game object.</param>
    /// <returns>Previously or newly attached component.</returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Checks whether a game object has a component of type T attached.
    /// </summary>
    /// <param name="gameObject">Game object.</param>
    /// <returns>True when component is attached.</returns>
    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() != null;
    }

    /// <summary>
    /// Returns true if the object has a rigidbody
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static bool HasRigidbody(this GameObject go)
    {
        return go.HasComponent<Rigidbody>();
    }

    /// <summary>
    /// Returns true if the object has an animation
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static bool HasAnimator(this GameObject go)
    {
        return go.HasComponent<Animator>();
    }

    /// <summary>
    /// Returns the Vector3 distance between these two GameObjects
    /// </summary>
    /// <param name="go"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static float DistanceTo(this GameObject go, GameObject target)
    {
        return Vector3.Distance(go.transform.position, target.transform.position);
    }

    /// <summary>
    /// Returns the Vector3 distance between the object and the position
    /// </summary>
    /// <param name="go"></param>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    public static float DistanceTo(this GameObject go, Vector3 targetPosition)
    {
        return Vector3.Distance(go.transform.position, targetPosition);
    }

    /// <summary>
    /// Sets the layer for the game object and all its children
    /// </summary>
    /// <param name="go"></param>
    /// <param name="layer"></param>
    /// <param name="recursively"></param>
    public static void SetLayer(this GameObject go, int layer, bool recursively = false)
    {
        go.layer = layer;

        if (!recursively) 
            return;

        foreach (Transform t in go.transform)
        {
            t.gameObject.SetLayer(layer, recursively);
        }
    }

    /// <summary>
    /// Enables or disables colliders on the game object and all its children
    /// </summary>
    /// <param name="go"></param>
    /// <param name="enabled"></param>
    public static void SetCollision(this GameObject go, bool enabled, bool recursively = false)
    {
        Collider GCollide = go.GetComponent<Collider>();
        if (GCollide != null)
            GCollide.enabled = enabled;

        if(recursively)
            foreach (Transform t in go.transform)
                t.gameObject.SetCollision(enabled, recursively);
    }
}
