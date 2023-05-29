using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Define.WorldObject WorldObjectType;
    public float hp;
    public float damage;
    public float moveSpeed;
    protected bool isDead;

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        UpdateMove();
    }

    public virtual void Init()
    {

    }

    protected abstract void UpdateMove();
    
    protected virtual void OnAttacked()
    {

    }

    public virtual void OnDamaged(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        if (isDead)
            return;

        isDead = true;
        SpawnManager.Instance.Despawn(gameObject);
    }
}
