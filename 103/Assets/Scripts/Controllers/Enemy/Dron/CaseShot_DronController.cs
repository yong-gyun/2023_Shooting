using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseShot_DronController : LongDistanceEnemyController
{
    Transform firePos;

    public override void Init()
    {
        if (GameManager.Instance.CurrentStage == 1)
        {
            hp = 24;
            attackSpeed = 4f;
            damage = 5f;
            score = 200;
        }
        else
        {
            hp = 50;
            attackSpeed = 4f;
            damage = 5f;
            score = 400;
        }

        firePos = transform.Find("FirePos");
        base.Init();
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
        Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, -30f)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy, Define.BulletMode.CaseShot);
        Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, 30f)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy, Define.BulletMode.CaseShot);
    }
}
