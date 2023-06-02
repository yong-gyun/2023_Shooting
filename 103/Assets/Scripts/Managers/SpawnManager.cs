using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get { Init(); return s_instance; } }
    static SpawnManager s_instance;

    public GameObject SpawnPool { get; set; }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@SpawnManager");

            if (go == null)
            {
                go = new GameObject("@SpawnManager");
                go.AddComponent<SpawnManager>();
            }

            s_instance = go.GetComponent<SpawnManager>();
            DontDestroyOnLoad(go);
        }
    }

    public GameObject Spawn(GameObject origin, Define.WorldObject type = Define.WorldObject.Enemy)
    {
        if (origin == null)
        {
            if (type == Define.WorldObject.Player)
                origin = Resources.Load<GameObject>("Prefabs/Player");
        }

        GameObject go = Instantiate(origin);

        switch (type)
        {
            case Define.WorldObject.Player:
                GameManager.Instance.SetPlayer(go);
                break;
            case Define.WorldObject.Boss:
                break;
        }

        go.GetComponent<BaseController>().WorldObjectType = type;
        return go;
    }

    public GameObject Spawn(string name, Define.WorldObject type = Define.WorldObject.Enemy)
    {
        GameObject origin = Resources.Load<GameObject>($"Prefabs/{type}/{name}");
        return Spawn(origin, type);
    }

    public GameObject Spawn(Define.WorldObject type)
    {
        if (type == Define.WorldObject.Player)
        {
            GameObject origin = Resources.Load<GameObject>("Prefabs/Player");
            return Spawn(origin, type);
        }
        else
        {
            GameObject origin = Resources.Load<GameObject>($"Prefabs/Stage{GameManager.Instance.CurrentStage}_Boss");
            return Spawn(origin, type);
        }

        return null;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        if (type == Define.WorldObject.Player)
        {
            Destroy(SpawnPool);
            GameManager.Instance.GameClear(false);
        }
        else
        {
            if (type == Define.WorldObject.Boss)
            {
                Destroy(SpawnPool);
                GameManager.Instance.GameClear(true);
            }

            GameManager.Instance.Score += go.GetComponent<EnemyController>().score;
        }

        Destroy(go);
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        return bc.WorldObjectType;
    }
}
