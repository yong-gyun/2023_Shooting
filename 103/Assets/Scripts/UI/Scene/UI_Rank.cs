using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Rank : UI_Scene
{
    enum Texts
    {
        Rank1,
        Rank2,
        Rank3,
        Rank4,
        Rank5,
        ScoreText
    }

    enum InputFields
    {
        NameInputField
    }

    enum Buttons
    {
        ConfirmButton
    }

    protected override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));

        Get<Button>((int)Buttons.ConfirmButton).onClick.AddListener(OnClickConfirmButton);
        TMP_Text[] rankTexts = new TMP_Text[5];

        rankTexts[0] = Get<TMP_Text>((int)Texts.Rank1);
        rankTexts[1] = Get<TMP_Text>((int)Texts.Rank2);
        rankTexts[2] = Get<TMP_Text>((int)Texts.Rank3);
        rankTexts[3] = Get<TMP_Text>((int)Texts.Rank4);
        rankTexts[4] = Get<TMP_Text>((int)Texts.Rank5);
        Get<TMP_Text>((int)Texts.ScoreText).text = $"Score : {GameManager.Instance.Score}";

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.Instance.scores[i] == 0)
            {
                rankTexts[i].text = "None";
                continue;
            }

            rankTexts[i].text = $"{i + 1}. {GameManager.Instance.names[i]} - {GameManager.Instance.scores[i]}";
        }
    }

    void OnClickConfirmButton()
    {
        SoundManager.Instance.Play("Click");
        string name = Get<TMP_InputField>((int)InputFields.NameInputField).text;
        GameManager.Instance.SortRank(name);
        SceneManagerEx.Instance.AsyncLoad(Define.Scene.Title);
    }
}