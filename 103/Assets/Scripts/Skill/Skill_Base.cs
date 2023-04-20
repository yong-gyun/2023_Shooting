using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public float CurrentCooltime;
    public float Cooltime;
    public int UseCount;
    [SerializeField] protected UI_Scene hud;

    public virtual void Init()
    {
        CurrentCooltime = 0;
        hud = UIManager.Instance.SceneUI; 
    }
}
