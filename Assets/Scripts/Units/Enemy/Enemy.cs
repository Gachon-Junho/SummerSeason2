using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Enemy : Unit, IPoolableObject, IHasPoint
{
    public abstract int Point { get; }
    
    public IObjectPool<GameObject> Pool { get; set; }

    public abstract int Type { get; }

    protected GameObject Player
    {
        get => player ??= GameObject.Find("Player");
    }


    private StageManager stageManager;
    private GameObject player;

    protected float MoveSpeed;
    protected float BulletSpeed;

    public void Initialize(int hp, float bulletSpeed, float attackSpeed, float moveSpeed)
    {
        base.Initialize(hp, 999, attackSpeed);
        MoveSpeed = moveSpeed;
        BulletSpeed = bulletSpeed;
    }

    public abstract void Initialize(Stage stage);

    // Update is called once per frame
    protected virtual void Update()
    {
        player ??= GameObject.FindWithTag("Player");
        
        var distance = Vector3.Distance(transform.position, player.transform.position);
        transform.Translate(-MoveSpeed, 0, 0);
        
        if (distance < 8)
        {
            var moveY = Vector3.MoveTowards(transform.position, player.transform.position, 0.01f).y;
            transform.position = new Vector3(transform.position.x, moveY);
        }
        
        if (transform.position.x < -23)
            Release();
    }

    public override void OnKilled(Unit killedUnit)
    {
    }

    protected override void OnDead()
    {
        Release();
    }

    public virtual void Release()
    {
        stageManager ??= GameObject.Find("StageManager").GetComponent<StageManager>();
        
        HP = MaxHP;
        Pool.Release(gameObject);
    }
}