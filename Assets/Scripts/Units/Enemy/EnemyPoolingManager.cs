using UnityEngine;

public class EnemyPoolingManager : ObjectPoolingManager<Enemy>
{
    [SerializeField]
    private GameObject[] prefabs;
    
    public int Count { get; private set; }

    protected override GameObject createPooledItem()
    {
        // 타입1과 2가 번갈아가며 순서대로 추가됩니다.
        Prefab = prefabs[Count % prefabs.Length];
        Count++;
        
        return base.createPooledItem();
    }

    private void shuffle()
    {
    }
}