using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseShot_DronController : DronController
{
    Transform firePos;

    public override void Init()
    {
        firePos = transform.Find("FirePos");
        WorldObjectType = Define.WorldObject.CaseShot_Dron;
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
