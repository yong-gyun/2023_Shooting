using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : EnemyController
{
    public float maxHp;
    [SerializeField] protected GameObject bulletOrigin;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected float height;
    [SerializeField] protected bool isArrive;
    [SerializeField] protected Transform[] explosions = new Transform[3];

    public override void Init()
    {
        bulletOrigin = Resources.Load<GameObject>($"Prefabs/Bullet/Stage{GameManager.Instance.CurrentStage}_BossBullet");
        firePos = transform.Find("FirePos");

        Transform explosionPoints = transform.Find("Explosions");

        for (int i = 0; i < explosionPoints.childCount; i++)
        {
            explosions[i] = explosionPoints.GetChild(i);
        }
        
        StartCoroutine(CoPattern());
        WorldObjectType = Define.WorldObject.Boss;
        height = 4.5f;
        score = 1000 * GameManager.Instance.CurrentStage;
        base.Init();
    }

    public override void OnDamaged(float damage)
    {
        if (!isArrive)
            return;

        base.OnDamaged(damage);
    }

    protected override void UpdateMove()
    {
        if (isArrive)
            return;

        if(transform.position.y <= height)
        {
            isArrive = true;
            UIManager.Instance.ShowPopupUI<UI_Boss>();
        }

        base.UpdateMove();
    }

    protected override void OnDead()
    {
        if (isDead)
            return;

        isDead = true;
        SoundManager.Instance.Stop();
        Destroy(SpawnManager.Instance.SpawnPool);
        StartCoroutine(DeadEffect());
    }

    protected abstract IEnumerator CoPattern();

    protected IEnumerator DeadEffect()
    {
        StopCoroutine(CoPattern());

        for (int i = 0; i < explosions.Length; i++)
        {
            if(GameManager.Instance.CurrentStage == 1)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Particle/MachineExplosion"));
                go.transform.position = explosions[i].transform.position;
                SoundManager.Instance.Play("Stage1_BossExplosion");
            }
            else
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Particle/AilenExplosion"));
                go.transform.position = explosions[i].transform.position;
                SoundManager.Instance.Play("PlayerHit");
            }

            Camera.main.GetComponent<CameraShake>().OnShake(0.05f, 0.2f);

            yield return new WaitForSeconds(0.75f);
        }

        while(transform.position.y < 9f)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            yield return null;
        }

        SpawnManager.Instance.Despawn(gameObject);
    }
}
