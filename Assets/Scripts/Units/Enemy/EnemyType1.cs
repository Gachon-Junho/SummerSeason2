using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : Enemy
{
    public override int Point => 10;

    public override int Type => 1;

    void Start()
    {
        
    }

    public override void Initialize(Stage stage) => Initialize(stage.E1HP, 0, 0, stage.E1MoveSpeed);

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
