using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct Rank
{
    public string name;
    public int score;

    public Rank(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { Init(); return s_instance; } }
    static GameManager s_instance;

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");

            if(go == null)
            {
                go = new GameObject("@GameManager");
                go.AddComponent<GameManager>();
            }

            s_instance = go.GetComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
    }

    private void Start()
    {
        Init();

        if (PlayerPrefs.HasKey("Rank_1"))
        {
            for (int i = 0; i < 5; i++)
            {
                scores[i] = PlayerPrefs.GetInt($"Rank_{i + 1}_Score");
                names[i] = PlayerPrefs.GetString($"Rank_{i + 1}_Name");
            }
        }
    }

    public float CurrentTime;
    public int CurrentStage;
    public GameObject SpawnPool;
    public int Score;
    public int[] scores = new int[5];
    public string[] names = new string[5];

    public GameObject GetPlayer()
    {
        return player;
    }

    [SerializeField] GameObject player;

    public GameObject Spawn(Define.WorldObject type)
    {
        string path = "";

        if (type == Define.WorldObject.Player)
            path = "Player";
        else
            path = $"Enemy/{type}";

        if (type == Define.WorldObject.Boss)
            path = $"Enemy/Stage{CurrentStage}_Boss";

        GameObject origin = Resources.Load<GameObject>($"Prefabs/{path}");
        GameObject go = Instantiate(origin);

        switch(type)
        {
            case Define.WorldObject.Player:
                player = go;
                break;
            case Define.WorldObject.Boss:
                break;
        }

        return go;
    }

    public void SortRank(string name)
    {
        int score = Score;
        for (int i = 0; i < scores.Length; i++)
        {
            for (int j = 0; j < scores.Length; j++)
            {
                if (scores[i] > scores[j])
                {
                    int i_temp = scores[i];
                    string s_temp = names[i];

                    scores[i] = scores[j];
                    names[i] = names[j];
                    scores[j] = i_temp;
                    names[j] = s_temp;
                }
            }
        }

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < score)
            {
                int i_temp = scores[i];
                string s_temp = names[i];

                scores[i] = score;
                names[i] = name;

                score = i_temp;
                name = s_temp;
            }
        }
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        if(type == Define.WorldObject.Player)
        {
            StartCoroutine(Faild());
        }
        else
        {
            if(type == Define.WorldObject.Boss)
            {
                StartCoroutine(CoStageClear());
            }

            Score += go.GetComponent<EnemyController>().score;
        }

        Destroy(go);
    }

    IEnumerator Faild()
    {
        yield return new WaitForSeconds(1);
        Destroy(SpawnPool);
        GameClear(false);
    }

    IEnumerator CoStageClear()
    {
        UIManager.Instance.CloseAllPopupUI();
        yield return new WaitForSeconds(1f);
        UIManager.Instance.ShowPopupUI<UI_StageClear>();
    }

    public void GameClear(bool isClear)
    {
        if(isClear)
        {

        }
        else
        {
            UIManager.Instance.ShowPopupUI<UI_Faild>();
        }
    }

    void OnApplicationQuit()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt($"Rank_{i + 1}_Score", scores[i]);
            PlayerPrefs.SetString($"Rank_{i + 1}_Name", names[i]);
        }
    }
}