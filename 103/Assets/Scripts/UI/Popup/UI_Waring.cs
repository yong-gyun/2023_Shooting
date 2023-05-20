using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Warning : UI_Popup
{
    enum Images
    {
        Image
    }

    float f_time = 1;
    float time = 0;

    protected override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        StartCoroutine(CoEffect());
        SoundManager.Instance.Play("Waring", Define.Sound.Bgm);
    }

    IEnumerator CoEffect()
    {
        Color color = Get<Image>((int) Images.Image).color;
        Debug.Log("a");
        for (int i = 0; i < 2; i++)
        {
            time = 0;

            while (color.a < 0.5f)
            {
                time += Time.deltaTime / f_time;
                color.a = Mathf.Lerp(0.2f, 0.5f, time);
                Get<Image>((int)Images.Image).color = color;
                yield return null;
            }

            time = 0;

            while (color.a > 0.2f)
            {
                time += Time.deltaTime / f_time;
                color.a = Mathf.Lerp(0.5f, 0.2f, time);
                Get<Image>((int)Images.Image).color = color;
                yield return null;
            }
        }

        UIManager.Instance.CloseAllPopupUI();
        GameManager.Instance.Spawn(Define.WorldObject.Boss).transform.position = new Vector3(0, 9, 0);
        SoundManager.Instance.Play("Boss", Define.Sound.Bgm);
    }
}
