using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cheat : UI_Popup
{
    enum Texts
    {
        Text
    }

    enum Images
    {
        Image
    }


    protected override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        DontDestroyOnLoad(gameObject);    
        base.Init();
    }

    public void SetInfo(string message)
    {
        Get<TMP_Text>((int)Texts.Text).text = message;
        StartCoroutine(Fadeout());
    }

    float time = 0;
    float f_time = 1f;

    IEnumerator Fadeout()
    {
        Color color = new Color(1, 1, 1, 0.5f);
        
        while (color.a > 0f)
        {
            time += Time.deltaTime / f_time;
            color.a = Mathf.Lerp(0.5f, 0, time);
            Get<Image>((int)Images.Image).color = new Color(0, 0, 0, color.a);
            Get<TMP_Text>((int)Texts.Text).color = color;
            yield return null;
        }

        Destroy(gameObject);
    }
}
