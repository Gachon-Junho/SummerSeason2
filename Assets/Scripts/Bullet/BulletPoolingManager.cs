using UnityEngine;

public class BulletPoolingManager : MonoSingleton<BulletPoolingManager>
{
    [SerializeField] 
    private BulletPool bulletPool;

    public Bullet Get() => bulletPool.Pool.Get().GetComponent<Bullet>();
}
