using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : UI_Scene
{
    enum Buttons
    {
        PlayButton,
        SettingButton,
        HelpButton,
        RankButton,
        ExitButton
    }

    protected override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.PlayButton).onClick.AddListener(OnClickPlayButton);
        Get<Button>((int)Buttons.SettingButton).onClick.AddListener(OnClickSettingButton);
        Get<Button>((int)Buttons.ExitButton).onClick.AddListener(OnClickExitButton);
        Get<Button>((int)Buttons.HelpButton).onClick.AddListener(OnClickHelpButton);
        Get<Button>((int)Buttons.RankButton).onClick.AddListener(OnClickRankButton);
    }

    void OnClickHelpButton()
    {
        UIManager.Instance.Clear();
        SceneManagerEx.Instance.Load(Define.Scene.Help);
        SoundManager.Instance.Play("Click");
    }

    void OnClickPlayButton()
    {
        GameManager.Instance.Score = 0;
        GameManager.Instance.CurrentTime = 0;
        SceneManagerEx.Instance.AsyncLoad(Define.Scene.Stage1);
        SoundManager.Instance.Play("Click");
    }

    void OnClickExitButton()
    {
        SoundManager.Instance.Play("Click");
        Application.Quit();
    }

    void OnClickRankButton()
    {
        SoundManager.Instance.Play("Click");
        SceneManagerEx.Instance.Load(Define.Scene.ViewRank);
    }

    void OnClickSettingButton()
    {
        SoundManager.Instance.Play("Click");
        bool onUI = FindObjectOfType<UI_Setting>();

        if (onUI)
            return;

        UIManager.Instance.ShowPopupUI<UI_Setting>();
    }
}
