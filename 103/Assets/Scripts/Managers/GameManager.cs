using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    public int CurrentStage { get; set; }
    public int Score { get; set; }
    public int[] scores = new int[5];
    public string[] names = new string[5];

    public GameObject GetPlayer()
    {
        return player;
    }

    public void SetPlayer(GameObject player) { this.player = player; }

    [SerializeField] GameObject player;

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

    public void GameClear(bool isClear)
    {
        if(isClear)
        {
            UIManager.Instance.CloseAllPopupUI();
            UIManager.Instance.ShowPopupUI<UI_StageClear>();
        }
        else
        {
            Destroy(SpawnManager.Instance.SpawnPool);
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