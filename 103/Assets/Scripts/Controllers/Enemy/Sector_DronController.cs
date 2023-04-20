using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_DronController : DronController
{
    Transform firePos;

    public override void Init()
    {
        firePos = transform.Find("FirePos");
        WorldObjectType = Define.WorldObject.Sector_Dron;
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
        Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, -25f)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
        Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, 25f)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
    }
}
