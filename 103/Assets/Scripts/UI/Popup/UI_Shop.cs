using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : UI_Popup
{
    enum GameObjects
    {
        Contents
    }

    protected override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        Transform root = Get<GameObject>((int)GameObjects.Contents).transform;

        for (int i = 0; i < 6; i++)
        {
            Define.ItemType type = (Define.ItemType) i;
            UI_ItemCardSubitem subitem =  UIManager.Instance.MakeSubitemUI<UI_ItemCardSubitem>(root);
            subitem.SetInfo(type);
        }
    }
}
