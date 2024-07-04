using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPoolableObject
{
    public IObjectPool<GameObject> Pool { get; set; }
    
    public float Speed = 1;
    public int Damage;
    public bool TrackTarget;
    public bool IsHited;

    public Unit Start => start;
    public Unit Target => target;

    private Unit start;
    private Unit target;
    
    private Vector3 origin;
    private Vector3 destination;
    
    public void Initialize(Unit start, Unit target, int damage, float speed = 1, bool trackTarget = false)
    {
        this.start = start;
        this.target = target;
        
        Damage = damage;
        Speed = speed;
        TrackTarget = trackTarget;

        origin = start.transform.position;
        destination = target.transform.position;
        IsHited = false;

        StartCoroutine(Fire());
    }

    public void Initialize(Unit start, Vector3 direction, int damage, float speed = 1, bool trackTarget = false)
    {
        this.start = start;
        origin = start.transform.position;
        destination = direction;
        
        Damage = damage;
        Speed = speed;
        TrackTarget = trackTarget;
        IsHited = false;
        
        StartCoroutine(Fire());
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual IEnumerator Fire()
    {
        while (!IsHited)
        {
            if (transform.position.x >= 20 || transform.position.x <= -20)
                break;
            
            var targetDirection = Vector3.MoveTowards(transform.position, destination, Speed);
            transform.position = new Vector3(transform.position.x + Mathf.Sign(destination.x - origin.x) * Speed, TrackTarget ? target.transform.position.y : transform.position.y);
            
            if (TrackTarget)
            {
                Vector2 newPos = target.transform.position - origin;
                float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                
                //transform.up = target.transform.position;
            }
                

            yield return null;
        }
        
        Release();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(start.tag) || other.CompareTag(tag))
            return;
        
        if (other.CompareTag("Guard"))
        {
            IsHited = true;
            return;
        }
            
        other.GetComponent<Unit>().DecreaseHP(Damage);
        IsHited = true;
    }

    public void Release() => Pool.Release(gameObject);
}