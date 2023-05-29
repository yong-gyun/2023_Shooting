using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAilen : DronController
{
    Transform firePos;

    public override void Init()
    {
        firePos = transform.Find("FirePos");
        base.Init();
    }

    protected override void OnAttacked()
    {
        if (isAttacked)
            return;

        StartCoroutine(CoAttacked());
        StartCoroutine(CoShot());
    }

    IEnumerator CoShot()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy, Define.BulletMode.Shoot);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamaged(float damage)
    {
        hp -= damage;

        if (hp <= 0)
            OnDead();

        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Self_Hit"), transform.position, Quaternion.identity);
    }

    protected override void OnDead()
    {
        if (isDead)
            return;

        isDead = true;
        
        GameObject go = SpawnManager.Instance.SpawnPool;

        if (go != null)
            go.GetComponent<SpawnPool>().isExist[spawnPoint] = false;

        int drop = Random.Range(1, 3);

        if (drop > 1)
        {
            int item = Random.Range(1, 100);

            if (item <= 30)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/FuelHeal"), transform.position, Quaternion.identity);
            }
            else if (item <= 45)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/AttackSpeed"), transform.position, Quaternion.identity);
            }
            else if (item <= 60)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/HpHeal"), transform.position, Quaternion.identity);
            }
            else if (item <= 75)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Damage"), transform.position, Quaternion.identity);
            }
            else if (item <= 90)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Shield"), transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/MoveSpeed"), transform.position, Quaternion.identity);
            }
        }

        SpawnManager.Instance.Despawn(gameObject);
        SoundManager.Instance.Play("Self_Die");
        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Self_AilenExplosion"), transform.position, Quaternion.identity);
    }
}
