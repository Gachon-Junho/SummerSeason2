using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : Enemy
{
    public override int Point => 20;

    public override int Type => 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Initialize(Stage stage)
    {
        Initialize(stage.E2HP, stage.E2BulletSpeed * 0.5f, stage.E2AttackSpeed, 0.0025f);
        StartCoroutine(fire());
    }

    private void OnEnable()
    {
        // 실행 순서에 주의하세요.
        // StartCoroutine(fire());
        // name = $"[2] {HP} {BulletSpeed} {AttackSpeed}";
    }

    private IEnumerator fire()
    {
        while (IsAlive)
        {
            var bullet = BulletPoolingManager.Current.Get();

            bullet.transform.position = transform.position;
            bullet.Initialize(this, Player.GetComponent<Player>(), Damage, BulletSpeed, true);

            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
