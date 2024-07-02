using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IPoolableObject
{
    public IObjectPool<GameObject> Pool { get; set; }
    
    public float Speed = 1;
    public int Damage;
    public bool TrackTarget;
    public bool IsHited;

    private Unit target;
    
    public void Initialize(Unit target, int damage, float speed = 1, bool trackTarget = false)
    {
        this.target = target;
        Damage = damage;
        Speed = speed;
        TrackTarget = trackTarget;

        StartCoroutine(Fire());
    }

    protected virtual IEnumerator Fire()
    {
        while (true)
        {
            if (IsHited)
                break;
            
            var targetDirection = Vector3.MoveTowards(transform.position, target.transform.position, Speed);
            transform.position = new Vector3(targetDirection.x, TrackTarget ? targetDirection.y : transform.position.y);
            
            if (TrackTarget)
                transform.LookAt(target.transform);

            yield return null;
        }
        
        target.DecreaseHP(Damage);
        Release();
    }

    public void Release() => Pool.Release(gameObject);
}