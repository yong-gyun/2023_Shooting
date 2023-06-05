using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_RankWindow : UI_Scene
{
    enum Texts
    {
        Rank1,
        Rank2,
        Rank3,
        Rank4,
        Rank5,
    }

    enum Buttons
    {
        MenuButton
    }

    protected override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.MenuButton).onClick.AddListener(() => { SoundManager.Instance.Play("Click", Define.Sound.Sfx); SceneManagerEx.Instance.AsyncLoad(Define.Scene.Title); });

        TMP_Text[] rankTexts = new TMP_Text[5];

        rankTexts[0] = Get<TMP_Text>((int)Texts.Rank1);
        rankTexts[1] = Get<TMP_Text>((int)Texts.Rank2);
        rankTexts[2] = Get<TMP_Text>((int)Texts.Rank3);
        rankTexts[3] = Get<TMP_Text>((int)Texts.Rank4);
        rankTexts[4] = Get<TMP_Text>((int)Texts.Rank5);

        List<RankData> ranks = DataManager.Instance.Ranks;

        for (int i = 0; i < 5; i++)
        {
            if (ranks[i].score == 0)
            {
                rankTexts[i].text = "None";
                continue;
            }

            rankTexts[i].text = $"{i + 1}. {ranks[i].name} - {ranks[i].score}";
        }
    }
}
