using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public int HP
    {
        get => hp;
        protected set => hp = value;
    }
    
    public int MaxHP
    {
        get => maxHP;
        protected set => maxHP = value;
    }
    
    public int Damage
    {
        get => damage;
        protected set => damage = value;
    }
    
    public float AttackSpeed
    {
        get => attackSpeed;
        protected set => attackSpeed = value;
    }
    
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

    public virtual void Initialize(int hp, int damage, float attackSpeed)
    {
        MaxHP = this.hp = hp;
        Damage = damage;
        AttackSpeed = attackSpeed;
    }

    private void Update()
    {
        TimeUntilAvailableAttack = Mathf.Clamp(TimeUntilAvailableAttack - Time.deltaTime, 0, float.MaxValue);
    }

    public abstract void OnKilled(Unit killedUnit);

    protected virtual void OnDead()
    {
    }

    public void DecreaseHP(int amount, Unit killer = null)
    {
        if (!IsAlive)
            return;
        
        hp = Math.Clamp(hp - amount, 0, maxHP);

        if (hp == 0)
        {
            OnDead();
            OnDied?.Invoke();
            
            // Propagate to killer
            killer?.OnKilled(this);
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

    private bool attack()
    {
        if (TimeUntilAvailableAttack > 0)
            return false;
        
        TimeUntilAvailableAttack = Time.time + 1 / attackSpeed;

        return true;
    }

    /// <summary>
    /// 다른 유닛에게 공격을 시도합니다.
    /// </summary>
    /// <param name="target">공격할 대상.</param>
    /// <returns>공격 성공 여부.</returns>
    public virtual bool Attack()
    {
        return attack();
    }
}
