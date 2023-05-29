using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Countdown : UI_Popup
{
    enum Texts
    {
        Text
    }

    protected override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        base.Init();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        Get<TMP_Text>((int)Texts.Text).text = "3";
        SoundManager.Instance.Play("Click");
        yield return new WaitForSeconds(1f);
        Get<TMP_Text>((int)Texts.Text).text = "2";
        SoundManager.Instance.Play("Click");
        yield return new WaitForSeconds(1f);
        Get<TMP_Text>((int)Texts.Text).text = "1";
        SoundManager.Instance.Play("Click");
        yield return new WaitForSeconds(1f);
        Get<TMP_Text>((int)Texts.Text).text = "Start";
        SoundManager.Instance.Play("Click");
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.ClosePopupUI();
        SpawnManager.Instance.SpawnPool = Instantiate(Resources.Load<GameObject>($"Prefabs/Contents/Stage{GameManager.Instance.CurrentStage}_SpawnPool"));
    }
}
