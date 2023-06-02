using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Straight_DronController : LongDistanceEnemyController
{
    Transform firePos;

    public override void Init()
    {
        if (GameManager.Instance.CurrentStage == 1)
        {
            hp = 20;
            attackSpeed = 3f;
            damage = 8f;
            score = 150;
        }
        else
        {
            hp = 40;
            attackSpeed = 3f;
            damage = 8f;
            score = 300;
        }

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
            Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
