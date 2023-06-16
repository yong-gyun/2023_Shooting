using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemCardSubitem : UI_Base
{
    enum Texts
    {
        GoldText,
        NameText
    }

    enum Button
    {
        UI_ItemCardSubitem
    }

    Define.Item type;

    protected override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
    }

    public void SetInfo(Define.Item type)
    {
        string name = "";
        int gold = 0;

        switch(type)
        {
            case Define.Item.Damage:
                name = "공격력\n업그레이드";
                gold = 15;
                break;
            case Define.Item.HpHeal:
                name = "체력 회복\n업그레이드";
                gold = 20;
                break;
            case Define.Item.FuelHeal:
                name = "연료 회복\n업그레이드";
                gold = 20;
                break;
            case Define.Item.AttackSpeed:
                name = "공격 속도\n업그레이드";
                gold = 10;
                break;
            case Define.Item.Shield:
                name = "보호막 생성";
                gold = 15;
                break;
            case Define.Item.MoveSpeed:
                name = "이동속도\n 업그레이드";
                gold = 12;
                break;
        }

        Get<TMP_Text>((int)Texts.GoldText).text = name;
        Get<TMP_Text>((int)Texts.NameText).text = $"{gold}G";
    }
}
