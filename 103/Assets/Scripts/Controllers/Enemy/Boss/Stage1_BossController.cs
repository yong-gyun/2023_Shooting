using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_BossController : BossController
{
    Transform []firePoints = new Transform[2];

    public override void Init()
    {
        Transform parent = transform.Find("FirePoints");

        for (int i = 0; i < firePoints.Length; i++)
        {
            firePoints[i] = parent.GetChild(i);
        }

        maxHp = 500f;
        hp = maxHp;
        damage = 15f;
        moveSpeed = 2;
        height = 3.5f;

        base.Init();
    }

    protected override void OnDead()
    {
        StopCoroutine(PatternA());
        StopCoroutine(PatternB());
        StopCoroutine(PatternC());
        StopCoroutine(PatternD());
        base.OnDead();
    }

    protected override IEnumerator CoPattern()
    {
        while(!isDead)
        {
            if(isArrive)
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(PatternA());
                yield return new WaitForSeconds(4f);
                StartCoroutine(PatternB());
                yield return new WaitForSeconds(4f);
                StartCoroutine(PatternC());
                yield return new WaitForSeconds(4f);
                StartCoroutine(PatternD());
                yield return new WaitForSeconds(2f);

            }

            yield return null;
        }
    }

    IEnumerator PatternA()
    {
        for (int i = 0; i < 8; i++)
        {
            if (isDead)
                yield break;
            Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
            Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, -20)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
            Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, 20)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
            Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, -40)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
            Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, 40)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator PatternB()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 360; j += 30)
            {
                if (isDead)
                    yield break;
                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, j)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator PatternC()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = -45; j <= 45; j += 15)
            {
                if (isDead)
                    yield break;
                Instantiate(bulletOrigin, firePoints[0].position, Quaternion.Euler(0, 0, j)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
                Instantiate(bulletOrigin, firePoints[1].position, Quaternion.Euler(0, 0, j * -1)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    IEnumerator PatternD()
    {
        if (isDead)
            yield break;
        Instantiate(bulletOrigin, firePoints[0].position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss, Define.BulletMode.CaseShot);
        Instantiate(bulletOrigin, firePoints[1].position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss, Define.BulletMode.CaseShot);

        yield return null;
    }
}
