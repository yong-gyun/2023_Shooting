using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            int percent = 100;

            if (item <= (int)Define.Item.Damage)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Damage"), transform.position, Quaternion.identity);
            }
            else if (item <= (int)Define.Item.HpHeal)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/HpHeal"), transform.position, Quaternion.identity);
            }
            else if (item <= (int)Define.Item.FuelHeal)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/FuelHeal"), transform.position, Quaternion.identity);
            }
            else if(item <= (int) Define.Item.AttackSpeed)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/AttackSpeed"), transform.position, Quaternion.identity);
            }
            else if(item <= (int)Define.Item.Shield)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Shield"), transform.position, Quaternion.identity);
            }
            else if(item <= (int)Define.Item.MoveSpeed)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/MoveSpeed"), transform.position, Quaternion.identity);
            }
            else if (item <= (int)Define.Item.Gold)
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Item/Gold"), transform.position, Quaternion.identity);
            }
        }

        SpawnManager.Instance.Despawn(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyWall"))
            Destroy(gameObject);

        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().OnDamaged(damage);
            Destroy(gameObject);
        }
    }
}
