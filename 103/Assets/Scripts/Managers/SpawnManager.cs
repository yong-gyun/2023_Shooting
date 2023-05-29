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
                go.AddComponent<GameManager>();
            }

            s_instance = go.GetComponent<SpawnManager>();
            DontDestroyOnLoad(go);
        }
    }

    public GameObject Spawn(Define.WorldObject type = Define.WorldObject.Enemy, GameObject origin = null)
    {
        if (origin == null)
            origin = Resources.Load<GameObject>("");

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

            }

            GameManager.Instance.Score += go.GetComponent<EnemyController>().score;
        }
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        return bc.WorldObjectType;
    }
}
