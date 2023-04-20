using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    public float attackSpeed;
    public float maxHp;
    public float maxFuel;
    public float fuel;
    public int fireIdx;

    [SerializeField] Transform[] firePos = new Transform[4];
    [SerializeField] GameObject bulletOrigin;

    Skill_Repair repair;
    Skill_Bomb bomb;

    bool isAttacked;
    bool isInvin;
    bool is_running_invin;

    protected override void Start()
    {
        tag = "Player";
        attackSpeed = 0.25f;
        maxHp = 100f;
        maxFuel = 100f;
        hp = maxHp;
        fuel = maxFuel;
        fireIdx = 2;
        damage = 5f;
        moveSpeed = 6.5f;
        bomb = GetComponent<Skill_Bomb>();
        repair = GetComponent<Skill_Repair>();

        bulletOrigin = Resources.Load<GameObject>("Prefabs/Bullet/PlayerBullet");

        Transform fireParent = transform.Find("FirePositions");

        for (int i = 0; i < fireParent.childCount; i++)
        {
            firePos[i] = fireParent.GetChild(i);
        }

        DontDestroyOnLoad(gameObject);

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(fuel <= 0)
        {
            fuel = 0;
            OnDead();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            OnAttacked();

        if (Input.GetKeyDown(KeyCode.R))
            bomb.UseSkill();

        if (Input.GetKeyDown(KeyCode.E))
            repair.UseSkill();

    }

    public override void Init()
    {
        bomb.Init();
        repair.Init();
    }

    protected override void UpdateMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = (Vector3.right * horizontal) + (Vector3.up * vertical);

        transform.position += dir * moveSpeed * Time.deltaTime;

        Vector3 viewport = Camera.main.WorldToViewportPoint(transform.position);

        if (viewport.x > 1)
            viewport.x = 1;
        else if (viewport.x < 0)
            viewport.x = 0;

        if (viewport.y > 1)
            viewport.y = 1;
        else if (viewport.y < 0)
            viewport.y = 0;

        transform.position = Camera.main.ViewportToWorldPoint(viewport);

        if (horizontal > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -25, 0), 90 * Time.deltaTime);
        else if (horizontal < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 25, 0), 90 * Time.deltaTime);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 90 * Time.deltaTime);

        if(GameManager.Instance.SpawnPool != null)
            fuel -= 1.5f * Time.deltaTime;
    }

    public override void OnDamaged(float damage)
    {
        if (isInvin || Cheat.invinCheat)
            return;

        hp -= damage;
        Camera.main.GetComponent<CameraShake>().OnShake(0.05f, 0.2f);

        bool exist = FindObjectOfType<UI_Hit>();

        if (!exist) 
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Hit"));

        if (hp <= 0)
        {
            hp = 0;
            OnDead();
        }
        else
        {
            SoundManager.Instance.Play("PlayerHit");
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        SoundManager.Instance.Play("Player_Die");
        SoundManager.Instance.Play("Faild", Define.Sound.Bgm);
    }

    protected override void OnAttacked()
    {
        if (isAttacked)
            return;

        for (int i = 0; i < fireIdx; i++)
        {
            Instantiate(bulletOrigin, firePos[i].position, Quaternion.identity).GetComponent<Bullet>().Init(damage, Define.BulletType.Player);
        }

        StartCoroutine(CoAttacked());
        SoundManager.Instance.Play("PlayerAttack");
    }

    public void InvinTrigger(float time)
    {
        if(is_running_invin)
        {
            StopCoroutine(CoInvin(time));
            //StopCoroutine(CoDamagedInvin());
        }

        StartCoroutine(CoInvin(time));
        //GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 1, 1f);
    }

    IEnumerator CoAttacked()
    {
        isAttacked = true;
        yield return new WaitForSeconds(attackSpeed);
        isAttacked = false;
    }

    IEnumerator CoInvin(float time)
    {
        isInvin = true;
        is_running_invin = true;
        yield return new WaitForSeconds(time);
        isInvin = false;
        is_running_invin = false;
    }

    //IEnumerator CoDamagedInvin()
    //{
    //    GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 1, 0.4f);
    //    isInvin = true;
    //    yield return new WaitForSeconds(2f);
    //    GetComponentInChildren<Renderer>().material.color = new Color(1, 1, 1, 1f);
    //    isInvin = false;
    //}
}
