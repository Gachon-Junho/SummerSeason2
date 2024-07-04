using System;

public class Stage
{
    public int E1Count { get; set; }
    public int E1HP { get; set; }
    public float E1MoveSpeed { get; set; }
    
    public int E2Count { get; set; }
    public int E2HP { get; set; }
    public float E2AttackSpeed { get; set; }
    public float E2BulletSpeed { get; set; }

    public int BossHP { get; set; }
    public float BossMoveSpeed { get; set; }
    public float BossAttackSpeed { get; set; }
    public float BossBulletSpeed { get; set; }

    public int EnemyCount => E1Count + E2Count;
}