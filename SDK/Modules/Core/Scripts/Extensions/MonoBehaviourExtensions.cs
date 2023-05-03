using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OGT;

public static class MonoBehaviourExtensions
{
    public static void SafeDestroy(this MonoBehaviour mono, GameObject target)
    {
        mono.gameObject.SafeDestroy(target);
    }
        
    public static void SafeDestroyListContent<T>(this MonoBehaviour mono, ref List<T> list, bool clean = true) where T : Component
    {
        list ??= new List<T>();
        var copy = new List<T>(list);
        foreach (T item in copy)
        {
            mono.SafeDestroy(item.gameObject);
        }
        if(clean)
            list.Clear();
    }
        
    public static void SafeDestroyListContent<TKey, TValue>(this MonoBehaviour mono, ref Dictionary<TKey, TValue> list, bool clean = true) where TKey : Component where TValue : Component
    {
        list ??= new Dictionary<TKey, TValue>();
        var copy = new Dictionary<TKey, TValue>(list);
        foreach (KeyValuePair<TKey, TValue> item in copy)
        {
            mono.SafeDestroy(item.Value.gameObject);
        }
        if(clean)
            list.Clear();
    }
        
    public static void SafeDestroyChildrenOf(this MonoBehaviour mono, ref Transform parent, Transform toIgnore = null) => parent.DestroyChildren(toIgnore);

    
    public static void ActionAfterFrame(this MonoBehaviour mono, Action action)
    {
        GameResources.Runtime.StartCoroutine(IEActionAfterFrame(action));
    }

    private static IEnumerator IEActionAfterFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }
    
    public static Coroutine ActionAfterSeconds(this MonoBehaviour mono, float seconds, Action action)
    {
        return GameResources.Runtime.StartCoroutine(IEActionAfterSeconds(seconds, action));
    }

    private static IEnumerator IEActionAfterSeconds(float seconds, Action action)
    {
        yield return new WaitForSecondsRealtime(seconds);
        action?.Invoke();
    }

    public static void ActionAfterCondition(this MonoBehaviour monoBehaviour, Func<bool> condition, Action action)
    {
        GameResources.Runtime.StartCoroutine(IEActionAfterCondition(condition, action));
    }

    private static IEnumerator IEActionAfterCondition(Func<bool> condition, Action action)
    {
        yield return new WaitUntil(condition);
        action?.Invoke();
    }

    public static void ActionAfterLoad<T>(this MonoBehaviour mono, string path, Action<T> action, Action onFailed = null) where T : UnityEngine.Object
    {
        GameResources.Runtime.StartCoroutine(IEActionAfterLoad(path, action, onFailed));
    }

    private static IEnumerator IEActionAfterLoad<T>(string path, Action<T> action, Action onFailed = null) where T : UnityEngine.Object
    {
        var request = Resources.LoadAsync<T>(path);
        yield return new WaitWhile(() => !request.isDone);
        if (request.asset == null)
        {
            onFailed?.Invoke();
        }
        else
        {
            action?.Invoke(request.asset as T);
        }
    }
}
