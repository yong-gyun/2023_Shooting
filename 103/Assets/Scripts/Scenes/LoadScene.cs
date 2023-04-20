using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : BaseScene
{
    protected override void Init()
    {
        UIManager.Instance.ShowSceneUI<UI_Load>();
        SoundManager.Instance.Stop();
    }
}