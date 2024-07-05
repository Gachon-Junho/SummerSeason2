using System.IO;
using UnityEngine;

public class EnemyPoolingManager : MonoSingleton<EnemyPoolingManager>
{
    [SerializeField]
    private EnemyPool E1Pool;
    
    [SerializeField]
    private EnemyPool E2Pool;

    public Enemy Get(int type)
    {
        switch (type)
        {
            case 1:
                return E1Pool.Pool.Get().GetComponent<Enemy>();
            
            case 2:
                return E2Pool.Pool.Get().GetComponent<Enemy>();
            
            default:
                throw new InvalidDataException();
        }
    }
}