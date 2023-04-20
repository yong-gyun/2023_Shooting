using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Scene : BaseScene
{
    bool isPause;

    protected override void Init()
    {
        GameManager.Instance.CurrentStage = 1;

        if(GameManager.Instance.GetPlayer() == null)
            GameManager.Instance.Spawn(Define.WorldObject.Player);

        UIManager.Instance.ShowSceneUI<UI_HUD>();
        UIManager.Instance.ShowPopupUI<UI_Countdown>();
        SoundManager.Instance.Play("Game", Define.Sound.Bgm);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                isPause = false;
                Time.timeScale = 1f;
                UIManager.Instance.ClosePopupUI();
            }
            else
            {
                isPause = true;
                Time.timeScale = 0f;
                UIManager.Instance.ShowPopupUI<UI_Pause>();
            }
        }

        GameManager.Instance.CurrentTime += Time.deltaTime;
    }
}