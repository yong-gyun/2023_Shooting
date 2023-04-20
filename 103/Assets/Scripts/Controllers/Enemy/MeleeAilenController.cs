using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAilenController : EnemyController
{
    Transform target;
    GameObject bulletOrigin;
    bool isArrive;

    public override void Init()
    {
        if(WorldObjectType == Define.WorldObject.Self_MeleeAilen)
        {
            bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/Self_MeleeAilenBullet");
        }
        else
        {
            bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/MeleeAilenBullet");
        }

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
        if(WorldObjectType == Define.WorldObject.Self_MeleeAilen)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            if (transform.position.y < 0f)
            {
                if (isArrive)
                    return;

                isArrive = true;
                for (int i = 0; i <= 360; i += 60)
                {
                    Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, i)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                }

                Destroy(gameObject);
            }
        }
        else
        {
            if (target == null)
                return;

            Vector3 dir = target.position - transform.position;
            dir.y = -1f;
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        }
    }

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
    
        if(WorldObjectType == Define.WorldObject.Self_MeleeAilen)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Self_Hit"), transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
        }
        else
        {
            Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Ailen_Hit"), transform.position, Quaternion.identity), 0.5f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        if(WorldObjectType != Define.WorldObject.Self_MeleeAilen)
        {
            if (WorldObjectType == Define.WorldObject.Cross_MeleeAilen)
            {
                Instantiate(bulletOrigin, transform.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 180)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 270)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            }
            else if (WorldObjectType == Define.WorldObject.Dia_MeleeAilen)
            {
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 45)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 135)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 225)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 315)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            }

            Instantiate(Resources.Load<GameObject>("Prefabs/Particle/AilenExplosion"), transform.position, Quaternion.identity);
            SoundManager.Instance.Play("Ailen_Die");
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Self_AilenExplosion"), transform.position, Quaternion.identity);
            SoundManager.Instance.Play("Self_Die");
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().OnDamaged(damage);
            Destroy(gameObject);
        }
    }
}
