using UnityEngine;
using UnityEngine.Pool;

public interface IPoolableObject
{
    public IObjectPool<GameObject> Pool { get; set; }
}