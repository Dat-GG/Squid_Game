using System;
using System.Collections;
using UnityEngine;

namespace Core.Common
{
    public static partial class Extensions
    {

        public static Coroutine StartDelayMethod(this MonoBehaviour mono, float time, Action callback)
        {
            return mono.StartCoroutine(Delay(time, callback));
        }

        static IEnumerator Delay(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            if (callback != null)
            {
                callback.Invoke();
            }
            else
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation only print log in UnityEditor
                Debug.LogWarning("Call back is destroyed");
            }
        }

        public static Coroutine StartDelayMethodRealTime(this MonoBehaviour mono, float time, Action callback)
        {
            return mono.StartCoroutine(DelayRealTime(time, callback));
        }

        static IEnumerator DelayRealTime(float time, Action callback)
        {
            yield return new WaitForSecondsRealtime(time);
            if (callback != null)
            {
                callback.Invoke();
            }
            else
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation only print log in UnityEditor
                Debug.LogWarning("Call back is destroyed");
            }
        }

        public static Coroutine StartDelayEndOfFrame(this MonoBehaviour mono, Action callback)
        {
            return mono.StartCoroutine(DelayFrame(callback));
        }

        static IEnumerator DelayFrame(Action callback)
        {
            yield return new WaitForEndOfFrame();
            if (callback != null)
            {
                callback.Invoke();
            }
            else
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation only print log in UnityEditor
                Debug.LogWarning("Call back is destroyed");
            }
        }
    }
}