using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScene : BaseScene
{
    protected override void Init()
    {
        UIManager.Instance.ShowSceneUI<UI_Help>();   
    }
}
