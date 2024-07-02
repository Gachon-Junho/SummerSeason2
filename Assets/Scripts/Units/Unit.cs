using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int HP => hp;
    public int MaxHP => maxHP;
    public int Damage => damage;
    public float AttackSpeed => attackSpeed;
    public bool IsAlive => hp > 0;

    public event Action OnDamaged;
    public event Action OnHealed;
    public event Action OnDied;
    
    [SerializeField] 
    private int hp;

    [SerializeField] 
    private int maxHP;

    [SerializeField] 
    private int damage;

    [SerializeField] 
    private float attackSpeed;

    protected float TimeUntilAvailableAttack { get; private set; }

    protected virtual void OnDead()
    {
    }

    public void DecreaseHP(int amount)
    {
        if (!IsAlive)
            return;
        
        hp = Math.Clamp(hp - amount, 0, maxHP);

        if (hp == 0)
        {
            OnDead();
            OnDied?.Invoke();
        }
            
        
        OnDamaged?.Invoke();
    }

    public void IncreaseHP(int amount)
    {
        if (!IsAlive)
            return;
        
        hp = Math.Clamp(hp + amount, 0, maxHP);
        
        OnHealed?.Invoke();
    }

    private bool attack(Unit target)
    {
        if (target == null || TimeUntilAvailableAttack > Time.time)
            return false;
        
        target.DecreaseHP(damage);
        TimeUntilAvailableAttack = Time.time + 1 / attackSpeed;

        return true;
    }

    /// <summary>
    /// 다른 유닛에게 공격을 시도합니다.
    /// </summary>
    /// <param name="target">공격할 대상.</param>
    /// <returns>공격 성공 여부.</returns>
    public virtual bool Attack(Unit target)
    {
        return attack(target);
    }
}
