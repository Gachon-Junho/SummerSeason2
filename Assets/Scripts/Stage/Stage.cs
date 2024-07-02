using System;

public class Stage : IEquatable<Stage>
{
    public int EnemyType1Count { get; set; }
    public int EnemyType2Count { get; set; }
    

    public bool Equals(Stage other)
    {
        return EnemyType1Count == other.EnemyType1Count && EnemyType2Count == other.EnemyType2Count;
    }

    public int EnemyCount => EnemyType1Count + EnemyType2Count;
}