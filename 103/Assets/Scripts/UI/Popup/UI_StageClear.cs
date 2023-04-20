using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StageClear : UI_Popup
{
    protected override void Init()
    {
        base.Init();
        StartCoroutine(CoClear());
        SoundManager.Instance.Play("Clear", Define.Sound.Bgm);
    }

    IEnumerator CoClear()
    {
        yield return new WaitForSeconds(4f);
        UIManager.Instance.ClosePopupUI();
        UIManager.Instance.ShowPopupUI<UI_StageResult>();
    }
}
