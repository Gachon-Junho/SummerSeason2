using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : Enemy
{
    public override int Point => 1000;

    public override int Type => 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Initialize(Stage stage) => Initialize(stage.BossHP, stage.BossBulletSpeed, stage.BossAttackSpeed, stage.BossMoveSpeed);

    protected override void OnDead()
    {
        var stageManager = GameObject.Find("StageManager");
        stageManager.GetComponent<StageManager>().UpdateStageState(0, true);
        
        GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().StartGenerate(null);
        Release();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Release() => Destroy(gameObject);
}
