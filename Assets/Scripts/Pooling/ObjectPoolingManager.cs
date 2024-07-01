using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ObjectPoolingManager<T> : MonoSingleton<ObjectPoolingManager<T>>
    where T : MonoBehaviour, IPoolableObject
{
    public IObjectPool<GameObject> Pool { get; private set; }
    
    public const int DEFAULT_CAPACITY = 10;
    public const int MAX_SIZE = 20;

    [SerializeField]
    public GameObject[] Prefabs;


    protected override void Awake()
    {
        base.Awake();

        Pool = new ObjectPool<GameObject>(createPooledItem, onGetFromPool, onReleaseToPool, onDestroyPoolObject, true, DEFAULT_CAPACITY, MAX_SIZE);
    }

    private GameObject createPooledItem()
    {
        var go = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);
        go.GetComponent<T>().Pool = Pool;

        return go;
    }
    
    private void onGetFromPool(GameObject poolGO) => poolGO.SetActive(true);
    
    private void onReleaseToPool(GameObject poolGO) => poolGO.SetActive(false);
    
    private void onDestroyPoolObject(GameObject poolGO) => Destroy(poolGO);
}
