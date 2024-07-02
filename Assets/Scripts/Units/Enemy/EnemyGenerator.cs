using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] 
    private StageManager stageManager;

    [SerializeField] 
    private GameObject EnemyType3Prefab;

    private IEnumerator runningCoroutine;

    public void StartGenerate(Button button)
    {
        button.gameObject.SetActive(false);
        
        if (runningCoroutine != null)
            StopCoroutine(runningCoroutine);
        
        StartCoroutine(runningCoroutine = GenerateEnemies());
    }

    IEnumerator GenerateEnemies()
    {
        yield return new WaitForSeconds(1);
        
        for (int i = 0; i <= stageManager.Current.EnemyCount; i++)
        {
            yield return new WaitForSeconds(1);

            if (stageManager.ShowBoss)
            {
                var boss = Instantiate(EnemyType3Prefab);
                boss.transform.position = new Vector3(12, Random.Range(-5 + boss.transform.localScale.y, 5 - boss.transform.localScale.y));

                continue;
            }
            
            var enemy = EnemyPoolingManager.Current.Pool.Get();
            stageManager.UpdateStageState(1, false);

            enemy.transform.position = new Vector3(12, Random.Range(-5 + enemy.transform.localScale.y, 5 - enemy.transform.localScale.y));
        }
    }
}
