using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILongDistanceAttack
{
    void Shot();
}

public class EnemyController : BaseController
{
    public int score;

    public override void Init()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        tag = "Enemy";
    }

    protected override void UpdateMove()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    protected override void OnDead()
    {
        if (isDead)
            return;

        isDead = true;

        int drop = Random.Range(1, 3);

        if(drop > 1)
        {
            int item = Random.Range(1, 100);

            if(item <= 30)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/FuelHeal"), transform.position, Quaternion.identity);
            }
            else if(item <= 45)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/AttackSpeed"), transform.position, Quaternion.identity);
            }
            else if(item <= 60)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/HpHeal"), transform.position, Quaternion.identity);
            }
            else if(item <= 75)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Damage"), transform.position, Quaternion.identity);
            }
            else if(item <= 90)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Shield"), transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/MoveSpeed"), transform.position, Quaternion.identity);
            }
        }

        SpawnManager.Instance.Despawn(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyWall"))
            Destroy(gameObject);
    }
}
