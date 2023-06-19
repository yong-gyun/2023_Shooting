using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] Define.ItemType type;
    float moveSpeed = 3f;
    PlayerController player;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * 180 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(type)
            {
                case Define.ItemType.Damage:
                    {
                        if (player.fireIdx >= 4)
                        {
                            if (player.damage >= 8f)
                                GameManager.Instance.Score += 50;
                            else
                                player.damage = 8f;
                        }
                        else
                        {
                            player.fireIdx++;
                        }
                    }
                    break;
                case Define.ItemType.HpHeal:
                    {
                        player.hp += 15f;

                        if (player.hp > player.maxHp)
                            player.hp = player.maxHp;
                    }
                    break;
                case Define.ItemType.FuelHeal:
                    {
                        player.fuel += 15f;

                        if (player.fuel > player.maxFuel)
                            player.fuel = player.maxFuel;
                    }
                    break;
                case Define.ItemType.AttackSpeed:
                    {
                        if (player.attackSpeed > 0.1f)
                            player.attackSpeed -= 0.025f;
                        else
                            GameManager.Instance.Score += 50;
                    }
                    break;
                case Define.ItemType.Shield:
                    {
                        player.InvinTrigger(2);

                        GameObject go = GameObject.Find("ShieldEffect(Clone)");

                        if(go != null)
                            Destroy(go);

                        Instantiate(Resources.Load<GameObject>("Prefabs/Subitem/ShieldEffect"), player.transform);
                    }
                    break;
                case Define.ItemType.MoveSpeed:
                    {
                        player.moveSpeed += 0.25f;
                    }
                    break;
                case Define.ItemType.Gold:
                    {
                        GameManager.Instance.CurrentGold++;
                    }
                    break;
            }

            Destroy(gameObject);
        }
    }
}
