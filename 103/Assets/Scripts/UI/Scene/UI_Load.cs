using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Load : UI_Scene
{
    enum Texts
    {
        LoadText
    }

    protected override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        StartCoroutine(CoEffect());
    }

    IEnumerator CoEffect()
    {
        string str = "Loading...";

        for (int i = 0; i < str.Length; i++)
        {
            Get<TMP_Text>((int)Texts.LoadText).text = str.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }

        SceneManagerEx.Instance.Load(SceneManagerEx.Instance.SceneBuffer);
    }
}
