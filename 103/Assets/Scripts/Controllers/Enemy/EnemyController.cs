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
        GameObject go = null;

        if (drop > 1)
        {
            int percent = Random.Range(1, 100);

            if (percent <= (int)Define.ItemPercent.Damage)
            {
                go = SpawnManager.Instance.SpawnItem(Define.ItemType.Damage);     
            }
            else if (percent <= (int)Define.ItemPercent.HpHeal)
            {
                go = SpawnManager.Instance.SpawnItem(Define.ItemType.HpHeal);
            }
            else if (percent <= (int)Define.ItemPercent.FuelHeal)
            {
                go = SpawnManager.Instance.SpawnItem(Define.ItemType.FuelHeal);
            }
            else if(percent <= (int) Define.ItemPercent.AttackSpeed)
            {
                go = SpawnManager.Instance.SpawnItem(Define.ItemType.AttackSpeed);
            }
            else if(percent <= (int)Define.ItemPercent.Shield)
            {
                go = SpawnManager.Instance.SpawnItem(Define.ItemType.Shield);
            }
            else if(percent <= (int)Define.ItemPercent.MoveSpeed)
            {
                go = SpawnManager.Instance.SpawnItem(Define.ItemType.MoveSpeed);
            }
        }
        else
        {
            go = SpawnManager.Instance.SpawnItem(Define.ItemType.Gold);
        }

        go.transform.position = transform.position;
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
