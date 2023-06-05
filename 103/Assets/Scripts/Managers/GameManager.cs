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
    }

    public float CurrentTime;
    public int CurrentStage { get; set; }
    public int Score { get; set; }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void SetPlayer(GameObject player) { this.player = player; }

    [SerializeField] GameObject player;

    

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
        DataManager.Instance.SaveData();
    }
}