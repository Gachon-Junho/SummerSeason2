using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T Current;

    protected virtual void Awake()
    {
        Current = this as T;
    }
}