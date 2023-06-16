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
                name = "���ݷ�\n���׷��̵�";
                gold = 15;
                break;
            case Define.Item.HpHeal:
                name = "ü�� ȸ��\n���׷��̵�";
                gold = 20;
                break;
            case Define.Item.FuelHeal:
                name = "���� ȸ��\n���׷��̵�";
                gold = 20;
                break;
            case Define.Item.AttackSpeed:
                name = "���� �ӵ�\n���׷��̵�";
                gold = 10;
                break;
            case Define.Item.Shield:
                name = "��ȣ�� ����";
                gold = 15;
                break;
            case Define.Item.MoveSpeed:
                name = "�̵��ӵ�\n ���׷��̵�";
                gold = 12;
                break;
        }

        Get<TMP_Text>((int)Texts.GoldText).text = name;
        Get<TMP_Text>((int)Texts.NameText).text = $"{gold}G";
    }
}
