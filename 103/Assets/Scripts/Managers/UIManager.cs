using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get { Init(); return s_instance; } }
    static UIManager s_instance;

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@UIManager");

            if (go == null)
            {
                go = new GameObject("@UIManager");
                go.AddComponent<UIManager>();
            }

            s_instance = go.GetComponent<UIManager>();
            DontDestroyOnLoad(go);
        }
    }

    public UI_Scene SceneUI;

    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    int order = 10;
    
    public void SetCanvas(GameObject go, bool sort)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        
        if(sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject origin = Resources.Load<GameObject>($"Prefabs/UI/Popup/{name}");
        T popup = Instantiate(origin).GetComponent<T>();
        popupStack.Push(popup);
        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject origin = Resources.Load<GameObject>($"Prefabs/UI/Scene/{name}");
        T scene = Instantiate(origin).GetComponent<T>();
        SceneUI = scene;
        return scene;
    }

    public void ClosePopupUI()
    {
        if(popupStack.Count > 0)
        {
            UI_Popup popup = popupStack.Pop();
            Destroy(popup.gameObject);
            order--;
        }
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count > 0)
        {
            if (popupStack.Peek() == popup)
                ClosePopupUI();
        }
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        SceneUI = null;
        CloseAllPopupUI();
    }
}
