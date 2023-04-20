using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageResult : UI_Popup
{
    public enum Texts
    {
        ScoreText,
        TimeText,
        TitleText,
    }

    public enum Buttons
    {
        ConfirmButton
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
        Get<TMP_Text>((int)Texts.TitleText).text = $"Stage {GameManager.Instance.CurrentStage} Clear !";
        Get<Button>((int)Buttons.ConfirmButton).onClick.AddListener(OnClickConfirmButton);
        SoundManager.Instance.Stop();

        PlayerController player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        GameManager.Instance.Score += (int)(player.hp + player.fuel) * 2;

        Destroy(GameManager.Instance.SpawnPool);
    }

    void OnClickConfirmButton()
    {
        SoundManager.Instance.Play("Click");

        if (GameManager.Instance.CurrentStage == 1)
        {
            UIManager.Instance.Clear();
            SceneManagerEx.Instance.Load(Define.Scene.Stage2);
        }
        else
        {
            SceneManagerEx.Instance.AsyncLoad(Define.Scene.Rank);
            Destroy(GameManager.Instance.GetPlayer());
        }
    }
}
