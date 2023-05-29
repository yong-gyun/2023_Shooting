using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dia_MeleeAilenController : EnemyController
{
    Transform target;
    GameObject bulletOrigin;

    public override void Init()
    {
            bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/MeleeAilenBullet");

        if(GameManager.Instance.CurrentStage == 1)
        {
            hp = 25f;
            moveSpeed = 2.5f;
            damage = 10f;
            score = 150;
        }
        else
        {
            hp = 50f;
            moveSpeed = 3f;
            damage = 15f;
            score = 400;
        }

        if (GameManager.Instance.GetPlayer() != null)
            target = GameManager.Instance.GetPlayer().transform;

        base.Init();
    }

    protected override void UpdateMove()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        dir.y = -1f;
        transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);

        Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Ailen_Hit"), transform.position, Quaternion.identity), 0.5f);
    }

    protected override void OnDead()
    {
        base.OnDead();
        Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 45)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 135)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 225)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 315)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy); Instantiate(Resources.Load<GameObject>("Prefabs/Particle/AilenExplosion"), transform.position, Quaternion.identity);
        SoundManager.Instance.Play("Ailen_Die");
    }
}
