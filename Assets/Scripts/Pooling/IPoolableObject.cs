using UnityEngine;
using UnityEngine.Pool;

public interface IPoolableObject
{
    IObjectPool<GameObject> Pool { get; set; }

    void Release();
}