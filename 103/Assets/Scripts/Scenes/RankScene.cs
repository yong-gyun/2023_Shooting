using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankScene : BaseScene
{
    protected override void Init()
    {
        UIManager.Instance.ShowSceneUI<UI_Rank>();
    }
}
