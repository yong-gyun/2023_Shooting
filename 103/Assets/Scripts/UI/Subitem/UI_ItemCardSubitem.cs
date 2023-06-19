using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ItemCardSubitem : UI_Base
{
    enum Texts
    {
        GoldText,
        NameText
    }

    enum Buttons
    {
        BuyButton
    }

    Define.ItemType type;
    int gold;

    protected override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
    }

    public void SetInfo(Define.ItemType type)
    {
        string name = "";
        int gold = 0;

        switch(type)
        {
            case Define.ItemType.Damage:
                name = "공격력\n업그레이드";
                gold = 15;
                break;
            case Define.ItemType.HpHeal:
                name = "체력 회복\n업그레이드";
                gold = 20;
                break;
            case Define.ItemType.FuelHeal:
                name = "연료 회복\n업그레이드";
                gold = 20;
                break;
            case Define.ItemType.AttackSpeed:
                name = "공격 속도\n업그레이드";
                gold = 10;
                break;
            case Define.ItemType.Shield:
                name = "보호막 생성";
                gold = 15;
                break;
            case Define.ItemType.MoveSpeed:
                name = "이동속도\n 업그레이드";
                gold = 12;
                break;
        }

        this.type = type;
        this.gold = gold;

        Get<TMP_Text>((int)Texts.NameText).text = name;
        Get<TMP_Text>((int)Texts.GoldText).text = $"{gold}G";
        Get<Button>((int)Buttons.BuyButton).onClick.AddListener(OnClickBuyItemButton);
    }

    void OnClickBuyItemButton()
    {
        if (GameManager.Instance.CurrentGold < gold)
            return;

        GameObject item = SpawnManager.Instance.SpawnItem(type);
        item.transform.position = GameManager.Instance.GetPlayer().transform.position;
        GameManager.Instance.CurrentGold -= gold;
    }
}
