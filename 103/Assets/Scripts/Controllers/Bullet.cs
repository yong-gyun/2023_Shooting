using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Define.BulletMode mode;
    [SerializeField] Define.BulletType type;
    [SerializeField] float damage;
    float moveSpeed;
    bool isShot;
    bool isHiting;

    public void Init(float damage, Define.BulletType type, Define.BulletMode mode = Define.BulletMode.Straight)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        
        this.damage = damage;
        this.type = type;
        this.mode = mode;

        Destroy(gameObject, 4f);

        switch (type)
        {
            case Define.BulletType.Player:
                {
                    if(damage >= 8f)
                    {
                        GetComponentInChildren<Renderer>().material.color = new Color(1, 0.4f, 0);
                    }

                    moveSpeed = 8f;
                }
                break;
            case Define.BulletType.Enemy:
                {
                    moveSpeed = 5f;
                    tag = "Bullet";
                }
                break;
            case Define.BulletType.Boss:
                {
                    moveSpeed = 8f;
                    tag = "Bullet";
                }
                break;
        }

        if(mode == Define.BulletMode.Shoot)
        {
            dir = GameManager.Instance.GetPlayer().transform.position - transform.position;
            dir = dir.normalized;
            dir.y = -1;
        }
    }
    [SerializeField] Vector3 dir;

    private void Update()
    {
        if(type == Define.BulletType.Player)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (mode == Define.BulletMode.Shoot)
            {
                transform.position += dir * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
        }

        if(mode == Define.BulletMode.CaseShot)
        {
            if (isShot)
                return;

            if (transform.position.y < 1f)
                Shot();
        }
    }

    void Shot()
    {
        isShot = true;

        for (int i = 0; i <= 360; i += 60)
        {
            Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, i)).GetComponent<Bullet>().Init(damage, type);
        }


        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletWall") || other.CompareTag("EnemyWall"))
            Destroy(gameObject);
    
        if(other.CompareTag("Enemy") && type == Define.BulletType.Player && !isHiting)
        {
            isHiting = true;
            other.GetComponent<BaseController>().OnDamaged(damage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") && type == Define.BulletType.Enemy && !isHiting)
        {
            other.GetComponent<PlayerController>().OnDamaged(damage);
            isHiting = true;
            Destroy(gameObject);
        }

        if(other.CompareTag("Player") && type == Define.BulletType.Boss && !isHiting)
        {
            other.GetComponent<PlayerController>().OnDamaged(damage);
            isHiting = true;
            Destroy(gameObject);
        }
    }
}
