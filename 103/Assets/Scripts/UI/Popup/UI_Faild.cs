using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Faild : UI_Popup
{
    enum Texts
    {
        ScoreText,
        TimeText,
    }

    enum Buttons
    {
        MenuButton,
        RetryButton
    }

    protected override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        int min = (int)GameManager.Instance.CurrentTime / 60;
        int sec = (int)GameManager.Instance.CurrentTime - min * 60;

        Get<TMP_Text>((int)Texts.TimeText).text = $"{min} : {sec}";
        Get<TMP_Text>((int)Texts.ScoreText).text = $"Score : {GameManager.Instance.Score}";

        Get<Button>((int)Buttons.MenuButton).onClick.AddListener(OnClickMenuButton);
        Get<Button>((int)Buttons.RetryButton).onClick.AddListener(OnClickRetryButton);
    }

    void OnClickRetryButton()
    {
        GameManager.Instance.Score = 0;
        GameManager.Instance.CurrentTime = 0;
        SoundManager.Instance.Play("Click");
        SceneManagerEx.Instance.Load(Define.Scene.Stage1);
    }

    void OnClickMenuButton()
    {
        SoundManager.Instance.Play("Click");
        SceneManagerEx.Instance.AsyncLoad(Define.Scene.Title);
    }
}
