using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Straight_DronController : DronController
{
    Transform firePos;

    public override void Init()
    {
        firePos = transform.Find("FirePos");
        WorldObjectType = Define.WorldObject.Straight_Dron;
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
