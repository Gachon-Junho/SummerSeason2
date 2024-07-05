using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] 
    private StageManager stageManager;

    [SerializeField]
    private GameObject enemyType3Prefab;

    private IEnumerator runningCoroutine;
    
    private int remainingE1Count;
    private int remainingE2Count;

    public void StartGenerate()
    {
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);
        
        StartCoroutine(runningCoroutine = GenerateEnemies());
    }

    IEnumerator GenerateEnemies()
    {
        if (stageManager.Current == null)
            yield break;
        
        yield return new WaitForSeconds(1);

        remainingE1Count = stageManager.Current.E1Count;
        remainingE2Count = stageManager.Current.E2Count;
        
        for (int i = 0; i <= stageManager.Current.EnemyCount; i++)
        {
            yield return new WaitForSeconds(1);

            if (stageManager.ShowBoss)
            {
                var boss = Instantiate(enemyType3Prefab);
                boss.transform.position = new Vector3(12, Random.Range(-5 + boss.transform.localScale.y, 5 - boss.transform.localScale.y));
                boss.GetComponent<Enemy>().Initialize(stageManager.Current);

                continue;
            }

            var enemy = getRandomEnemy();
            enemy.transform.position = new Vector3(12, Random.Range(-5 + enemy.transform.localScale.y, 5 - enemy.transform.localScale.y));
            enemy.GetComponent<Enemy>().Initialize(stageManager.Current);
            
            stageManager.UpdateStageState(1, false);
        }
    }

    private Enemy getRandomEnemy()
    {
        if (remainingE1Count + remainingE2Count < 1)
            throw new IndexOutOfRangeException();
        
        var type = Random.Range(remainingE1Count > 0 ? 1 : 3, remainingE2Count > 0 ? 3 : 1);
        var enemy = EnemyPoolingManager.Current.Get(type);

        switch (type)
        {
            case 1:
                remainingE1Count--;
                break;
            
            case 2:
                remainingE2Count--;
                break;
        }

        return enemy;
    }
}
