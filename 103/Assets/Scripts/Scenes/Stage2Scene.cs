using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Scene : BaseScene
{
    protected override void Init()
    {
        SoundManager.Instance.Play("Game", Define.Sound.Bgm);
        GameManager.Instance.CurrentStage = 2;
        UIManager.Instance.ShowSceneUI<UI_HUD>();
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().Init();
        
        UIManager.Instance.ShowPopupUI<UI_Countdown>();
    }

    bool isPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
