using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Scene : BaseScene
{
    bool isPause;
    UI_Shop shopUI;

    protected override void Init()
    {
        SoundManager.Instance.Play("Game", Define.Sound.Bgm);
        GameManager.Instance.CurrentStage = 2;
        UIManager.Instance.ShowSceneUI<UI_HUD>();
        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().Init();
        
        UIManager.Instance.ShowPopupUI<UI_Countdown>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopUI == null)
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

        if (Input.GetKeyDown(KeyCode.B) && isPause == false)
        {
            if (shopUI)
            {
                UIManager.Instance.ClosePopupUI();
            }
            else
            {
                shopUI = UIManager.Instance.ShowPopupUI<UI_Shop>();
            }
        }

        if (isPause == false)
            GameManager.Instance.CurrentTime += Time.deltaTime;
    }
}
