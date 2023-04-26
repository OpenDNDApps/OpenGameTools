using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
    public static class MonoBehaviourExtensions
    {
        public static void ActionAfterFrame(this MonoBehaviour monoBehaviour, Action action)
        {
            VGDevsRuntime.Instance.StartCoroutine(IEActionAfterFrame(action));
        }

        private static IEnumerator IEActionAfterFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
        
        public static void ActionAfterSeconds(this MonoBehaviour monoBehaviour, float seconds, Action action)
        {
            VGDevsRuntime.Instance.StartCoroutine(IEActionAfterSeconds(seconds, action));
        }

        private static IEnumerator IEActionAfterSeconds(float seconds, Action action)
        {
            yield return new WaitForSecondsRealtime(seconds);
            action?.Invoke();
        }

        public static void ActionAfterCondition(this MonoBehaviour monoBehaviour, Func<bool> condition, Action action)
        {
            VGDevsRuntime.Instance.StartCoroutine(IEActionAfterCondition(condition, action));
        }

        private static IEnumerator IEActionAfterCondition(Func<bool> condition, Action action)
        {
            yield return new WaitUntil(condition);
            action?.Invoke();
        }
    }
}
