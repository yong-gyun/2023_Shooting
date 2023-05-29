using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DronController : EnemyController
{
    protected GameObject bulletOrigin;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected bool isAttacked;
    protected int spawnPoint;

    public void SetPoint(int idx) { spawnPoint = idx; }

    public override void Init()
    {
        base.Init();
        bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/DronBullet");

        moveSpeed = 3.5f;
    }

    protected override void UpdateMove()
    {
        if(transform.position.y > 4.5f)
            base.UpdateMove();

        OnAttacked();
    }

    protected IEnumerator CoAttacked()
    {
        isAttacked = true;
        yield return new WaitForSeconds(attackSpeed);
        isAttacked = false;
    }

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Dron_Hit"), transform.position, Quaternion.identity);
    }

    protected override void OnDead()
    {
        base.OnDead();

        GameObject go = SpawnManager.Instance.SpawnPool;
        
        if(go != null)
            go.GetComponent<SpawnPool>().isExist[spawnPoint] = false;
        
        SoundManager.Instance.Play("Dron_Die");
        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/MachineExplosion"), transform.position, Quaternion.identity);
    }
}