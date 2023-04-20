using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DronController : EnemyController
{
    protected GameObject bulletOrigin;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected bool isAttacked;
    protected int spawnPoint;

    public void SetPoint(int idx) => spawnPoint = idx;

    public override void Init()
    {
        base.Init();
        bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/DronBullet");

        if(GameManager.Instance.CurrentStage == 1)
        {
            switch (WorldObjectType)
            {
                case Define.WorldObject.Sector_Dron:
                    {
                        hp = 25;
                        attackSpeed = 3.5f;
                        damage = 8f;
                        score = 200;
                    }
                    break;
                case Define.WorldObject.Double_Dron:
                    {
                        hp = 30;
                        attackSpeed = 5f;
                        damage = 5f;
                        score = 300;
                    }
                    break;
                case Define.WorldObject.Straight_Dron:
                    {
                        hp = 20;
                        attackSpeed = 3f;
                        damage = 8f;
                        score = 150;
                    }
                    break;
                case Define.WorldObject.CaseShot_Dron:
                    {
                        hp = 24;
                        attackSpeed = 4f;
                        damage = 5f;
                        score = 200;
                    }
                    break;
            }
        }
        else
        {
            switch (WorldObjectType)
            {
                case Define.WorldObject.Sector_Dron:
                    {
                        hp = 60;
                        attackSpeed = 3.5f;
                        damage = 8f;
                        score = 400;
                    }
                    break;
                case Define.WorldObject.Double_Dron:
                    {
                        hp = 80;
                        attackSpeed = 5f;
                        damage = 5f;
                        score = 500;
                    }
                    break;
                case Define.WorldObject.Straight_Dron:
                    {
                        hp = 40;
                        attackSpeed = 3f;
                        damage = 8f;
                        score = 300;
                    }
                    break;
                case Define.WorldObject.CaseShot_Dron:
                    {
                        hp = 50;
                        attackSpeed = 4f;
                        damage = 5f;
                        score = 400;
                    }
                    break;
            }
        }
        

        moveSpeed = 3.5f;

    }

    protected override void UpdateMove()
    {
        if(transform.position.y > 4.5f)
            base.UpdateMove();

        OnAttacked();
    }

    protected IEnumerator CoAttacked()
    {
        isAttacked = true;
        yield return new WaitForSeconds(attackSpeed);
        isAttacked = false;
    }

    public override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/Dron_Hit"), transform.position, Quaternion.identity);
    }

    protected override void OnDead()
    {
        base.OnDead();

        GameObject go = GameManager.Instance.SpawnPool;
        
        if(go != null)
            go.GetComponent<SpawnPool>().isExist[spawnPoint] = false;
        
        SoundManager.Instance.Play("Dron_Die");
        Instantiate(Resources.Load<GameObject>("Prefabs/Particle/MachineExplosion"), transform.position, Quaternion.identity);
    }
}