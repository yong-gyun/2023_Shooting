using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : EnemyController
{
    public override void Init()
    {
        base.Init();

        if(GameManager.Instance.CurrentStage == 1)
        {
            damage = 15f;
            moveSpeed = 5f;
            hp = 20f;
            score = 200;
        }
        else
        {

            damage = 20f;
            moveSpeed = 6f;
            hp = 40f;
            score = 400;
        }
    }

    protected override void UpdateMove()
    {
        base.UpdateMove();
        transform.Rotate((Vector3.down + Vector3.right) * 90 * Time.deltaTime);
    }

    protected override void OnDead()
    {
        base.OnDead();
        Instantiate(Resources.Load("Prefabs/Particle/MeteorExplosion"), transform.position, Quaternion.identity);
    }
}
