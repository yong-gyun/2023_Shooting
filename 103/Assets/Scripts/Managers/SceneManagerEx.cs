using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : MonoBehaviour
{
    public static SceneManagerEx Instance { get { Init(); return s_instance; } }
    static SceneManagerEx s_instance;

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@SceneManagerEx");

            if (go == null)
            {
                go = new GameObject("@SceneManagerEx");
                go.AddComponent<SceneManagerEx>();
            }

            s_instance = go.GetComponent<SceneManagerEx>();
            DontDestroyOnLoad(go);
        }
    }

    public void Load(Define.Scene type)
    {
        UIManager.Instance.Clear();
        SceneManager.LoadScene(type.ToString());
    }

    public Define.Scene SceneBuffer;

    public void AsyncLoad(Define.Scene type)
    {
        SceneBuffer = type;
        Load(Define.Scene.Load);
    }
}
