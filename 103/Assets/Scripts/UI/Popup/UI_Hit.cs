using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hit : UI_Popup
{
    protected override void Init()
    {
        base.Init();
        Destroy(gameObject, 0.1f);
    }
}
