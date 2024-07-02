using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Enemy : Unit, IPoolableObject, IHasPoint
{
    public abstract int Point { get; }
    
    public IObjectPool<GameObject> Pool { get; set; }

    public abstract int Type { get; }
    
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        player ??= GameObject.FindWithTag("Player");
        
        var distance = Vector3.Distance(transform.position, player.transform.position);
        transform.Translate(-0.01f, 0, 0);
        
        if (distance < 8)
        {
            transform.Translate(0, 0, 0);
            var moveY = Vector3.MoveTowards(transform.position, player.transform.position, 0.01f).y;
            transform.position = new Vector3(transform.position.x, moveY);
            //transform.LookAt(player.transform);
        }
        
        if (transform.position.x < -23)
            Release();
    }

    public virtual void Release() => Pool.Release(gameObject);
}
