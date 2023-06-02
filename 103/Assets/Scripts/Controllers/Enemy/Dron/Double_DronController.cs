using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_DronController : LongDistanceEnemyController
{
    Transform []firePos = new Transform[2];

    public override void Init()
    {
        if (GameManager.Instance.CurrentStage == 1)
        {
            hp = 30;
            attackSpeed = 5f;
            damage = 5f;
            score = 300;
        }
        else
        {
            hp = 80;
            attackSpeed = 5f;
            damage = 5f;
            score = 500;
        }

        Transform fireParent = transform.Find("FirePositions");

        for (int i = 0; i < fireParent.childCount; i++)
        {
            firePos[i] = fireParent.GetChild(i);
        }

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

        for (int i = 0; i < 5; i++)
        {
            Instantiate(bulletOrigin, firePos[0].position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy, Define.BulletMode.Shoot);
            Instantiate(bulletOrigin, firePos[1].position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy, Define.BulletMode.Shoot);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
