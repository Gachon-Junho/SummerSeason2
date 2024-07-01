using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Enemy : Unit, IPoolableObject, IHasPoint
{
    public abstract int Point { get; }
    
    public IObjectPool<GameObject> Pool { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
