using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class EnemyType3 : Enemy
{
    public override int Point => 1000;

    public override int Type => 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Initialize(Stage stage)
    {
        Initialize(stage.BossHP, stage.BossBulletSpeed, stage.BossAttackSpeed, stage.BossMoveSpeed * 0.5f);
        StartCoroutine(fire());
    }

    private IEnumerator fire()
    {
        while (IsAlive)
        {
            var count = Random.Range(10, 20);
            
            for (int i = 0; i < count; i++)
            {
                var bullet = BulletPoolingManager.Current.Get();

                var dir = new Vector2(BulletSpeed * Mathf.Cos(Mathf.PI * 2 * i / count), BulletSpeed * Mathf.Sin(Mathf.PI * i * 2 / count));
                bullet.transform.Rotate(new Vector3(0f, 0f, 360f * i / count - 90));
                bullet.transform.position = transform.position;
                bullet.Initialize(this, dir, Damage, BulletSpeed);
            }

            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    protected override void OnDead()
    {
        var stageManager = GameObject.Find("StageManager");
        stageManager.GetComponent<StageManager>().UpdateStageState(0, true);
        
        GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().StartGenerate();
        Release();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Release() => Destroy(gameObject);
}