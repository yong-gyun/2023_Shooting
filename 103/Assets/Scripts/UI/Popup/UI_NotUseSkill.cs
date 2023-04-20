using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NotUseSkill : UI_Popup
{
    protected override void Init()
    {
        base.Init();
        Destroy(gameObject, 0.5f);
        SoundManager.Instance.Play("SkillWaring");
    }
}