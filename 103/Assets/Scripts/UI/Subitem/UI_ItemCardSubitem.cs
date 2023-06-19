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
                name = "���ݷ�\n���׷��̵�";
                gold = 15;
                break;
            case Define.ItemType.HpHeal:
                name = "ü�� ȸ��\n���׷��̵�";
                gold = 20;
                break;
            case Define.ItemType.FuelHeal:
                name = "���� ȸ��\n���׷��̵�";
                gold = 20;
                break;
            case Define.ItemType.AttackSpeed:
                name = "���� �ӵ�\n���׷��̵�";
                gold = 10;
                break;
            case Define.ItemType.Shield:
                name = "��ȣ�� ����";
                gold = 15;
                break;
            case Define.ItemType.MoveSpeed:
                name = "�̵��ӵ�\n ���׷��̵�";
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
