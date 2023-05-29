using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_BossController : BossController
{
    public override void Init()
    {
        maxHp = 1000f;
        moveSpeed = 2.5f;
        damage = 20f;
        hp = maxHp;
        height = 3f;

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
                StartCoroutine(PatternE());
                yield return new WaitForSeconds(4f);
                StartCoroutine(PatternF());
                yield return new WaitForSeconds(4f);
                StartCoroutine(PatternA());
                yield return new WaitForSeconds(4f);
                StartCoroutine(PatternB());
                yield return new WaitForSeconds(6f);
                StartCoroutine(PatternC());
                yield return new WaitForSeconds(6f);
                StartCoroutine(PatternD());
            }

            yield return null;
        }
    }

    IEnumerator PatternE()
    {
        if (isDead)
            yield break;
        Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss, Define.BulletMode.CaseShot);

        yield return new WaitForSeconds(1f);
        Instantiate(bulletOrigin, firePos.position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss, Define.BulletMode.CaseShot);

    }

    IEnumerator PatternF()
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

    IEnumerator PatternA()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j <= 360; j += 45)
            {
                if (isDead)
                    yield break;

                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, j)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            }

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

                Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, j)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    IEnumerator PatternC()
    {
        for (int i = 0; i < 4; i++)
        {
            if (isDead)
                yield break;

            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 45)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 135)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 225)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 315)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            if (isDead)
                yield break;

            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 180)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 270)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            if (isDead)
                yield break;

            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 45)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 135)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 225)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 315)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            if (isDead)
                yield break;

            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 180)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);
            Instantiate(bulletOrigin, transform.position, Quaternion.Euler(0, 0, 270)).GetComponent<Bullet>().Init(damage, Define.BulletType.Enemy);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator PatternD()
    {
        for (int i = 0; i < 3; i++)
        {
            if (isDead)
                yield break;

            for (int j = -45; j <= 45; j += 15)
            {
                Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, j)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
                yield return new WaitForSeconds(0.1f);
            }

            for (int j = 45; j >= -45; j -= 15)
            {
                Instantiate(bulletOrigin, firePos.position, Quaternion.Euler(0, 0, j)).GetComponent<Bullet>().Init(damage, Define.BulletType.Boss);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
