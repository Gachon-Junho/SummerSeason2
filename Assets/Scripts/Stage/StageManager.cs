using System;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private CSVStorage csvs;
    
    public Stage[] Stages { get; private set; }
    public Stage Current => Stages == null || Stages.Length == 0 ? null : Stages[CurrentStageIndex];

    public int RemainingEnemies => Current == null ? 0 : Current.EnemyType1Count + Current.EnemyType2Count - GeneratedEnemies;
    public bool ShowBoss => RemainingEnemies == 0;

    public int GeneratedEnemies { get; private set; }
    public int CurrentStageIndex { get; private set; }
    
    void Awake()
    {
        csvs = new CSVStorage();

        Stages = csvs.Read<Stage>(CSVStorage.STAGE_FILE_NAME).ToArray();
    }

    public void UpdateStageState(int generatedCount, bool killedBoss)
    {
        GeneratedEnemies += generatedCount;

        if (killedBoss)
        {
            CurrentStageIndex++;
            GeneratedEnemies = 0;
        }
    }
}