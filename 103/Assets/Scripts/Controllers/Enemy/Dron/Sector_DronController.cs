using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_DronController : LongDistanceEnemyController
{
    Transform firePos;

    public override void Init()
    {
        firePos = transform.Find("FirePos");
        base.Init();

        if(GameManager.Instance.CurrentStage == 1)
        {
            hp = 25;
            attackSpeed = 3.5f;
            damage = 8f;
            score = 200;
        }
        else
        {
            hp = 60;
            attackSpeed = 3.5f;
            damage = 8f;
            score = 400;
        }
    }

    protected override void OnAttacked()
    {
        if (isAttacked)
            return;

        StartCoroutine(CoAttacked());
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, -25f)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, 25f)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
    }
}
