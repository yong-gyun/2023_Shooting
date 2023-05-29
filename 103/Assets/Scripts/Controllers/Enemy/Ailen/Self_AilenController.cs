using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_AilenController : EnemyController
{
    GameObject bulletOrigin;
    bool isArrive;

    protected override void UpdateMove()
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

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Self_Hit"), transform.position, Quaternion.identity);
        Destroy(go, 0.5f);
    }

    protected override void OnDead()
    {
        base.OnDead();

        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Self_AilenExplosion"), transform.position, Quaternion.identity);
        SoundManager.Instance.Play("Self_Die");
    }
}
