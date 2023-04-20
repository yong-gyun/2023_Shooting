using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if(recursive)
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (component.name == name || string.IsNullOrEmpty(name))
                    return component;
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                T component = transform.GetComponent<T>();
                
                if(component != null)
                {
                    if (string.IsNullOrEmpty(name) || component.name == name)
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false)
    {
        Transform transoform = FindChild<Transform>(go, name, recursive);
        return transoform.gameObject;
    }
}
