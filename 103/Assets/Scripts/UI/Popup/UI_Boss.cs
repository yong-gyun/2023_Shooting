using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boss : UI_Popup
{
    enum Texts
    {
        HPText
    }

    enum Scrollbars
    {
        HP
    }

    BossController boss;

    protected override void Init()
    {
        boss = FindObjectOfType<BossController>();
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Scrollbar>(typeof(Scrollbars));
    }

    private void Update()
    {
        Get<TMP_Text>((int)Texts.HPText).text = $"{boss.hp}";
        Get<Scrollbar>((int)Scrollbars.HP).size = boss.hp / boss.maxHp;
    }
}
