using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResourceData
{
    public UnityEngine.Object Data;
    public string Path;

    public ResourceData(UnityEngine.Object data, string path)
    {
        Data = data;
        Path = path;
    }
}

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get { Init(); return s_instance; } }
    static ResourceManager s_instance;

    public List<ResourceData> Resources { get; private set; } = new List<ResourceData>();

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@ResourceManager");

            if (go == null)
            {
                go = new GameObject("@ResourceManager");
                go.AddComponent<ResourceManager>();
            }

            s_instance = go.GetComponent<ResourceManager>();
            DontDestroyOnLoad(go);
        }
    }
}
