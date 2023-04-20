using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Help : UI_Scene
{
    enum Buttons
    {
        MenuButton
    }

    protected override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.MenuButton).onClick.AddListener(() => { SoundManager.Instance.Play("Click"); SceneManagerEx.Instance.Load(Define.Scene.Title); });
    }
}
