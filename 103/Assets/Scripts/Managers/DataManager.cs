using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get { Init(); return s_instance; } }
    static DataManager s_instance;

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@DataManager");

            if (go == null)
            {
                go = new GameObject("@DataManager");
                go.AddComponent<DataManager>();
            }

            s_instance = go.GetComponent<DataManager>();
            DontDestroyOnLoad(go);
        }
    }

    public List<RankData> Ranks = new List<RankData>();

    private void Awake()
    {
        Init();
        LoadData();
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("Rank_1"))
        {
            for (int i = 0; i < 5; i++)
            {
                RankData data = new RankData()
                {
                    name = PlayerPrefs.GetString($"Rank_{i + 1}_Name"),
                    score = PlayerPrefs.GetInt($"Rank_{i + 1}_Score"),
                };

                Ranks.Add(data);
            }
        }
    }

    public void SaveData()
    {
        for (int i = 0; i < Ranks.Count; i++)
        {
            if (i >= 5)
                break;

            PlayerPrefs.SetInt($"Rank_{i + 1}_Score", Ranks[i].score);
            PlayerPrefs.SetString($"Rank_{i + 1}_Name", Ranks[i].name);
        }
    }
}
