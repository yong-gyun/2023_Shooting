using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Bomb : Skill_Base
{
    public override void Init()
    {
        UseCount = 5;
        Cooltime = 10f;
        base.Init();
    }

    public void UseSkill()
    {
        if (CurrentCooltime > 0 || UseCount == 0)
        {
            bool exist = FindObjectOfType<UI_UnUseableSkill>();

            if(exist == false)
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_NotUseSkill"));

            return;
        }

        SoundManager.Instance.Play("Bomb");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        EnemyController[] enemys = FindObjectsOfType<EnemyController>();

        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].OnDamaged(40f);
        }

        UseCount--;
        hud.Get<TMP_Text>((int)UI_HUD.Texts.BombUseCountText).text = $"{UseCount}";

        if (UseCount > 0)
        {
            StartCoroutine(CoCooltime());
        }
        else
        {
            hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Bomb).size = 1;
        }
    }

    IEnumerator CoCooltime()
    {
        CurrentCooltime = Cooltime;
        hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Bomb).GetComponent<Image>().color = Color.gray;
        hud.Get<TMP_Text>((int)UI_HUD.Texts.BombCooltimeText).gameObject.SetActive(true);

        while (CurrentCooltime >= 0)
        {
            hud.Get<TMP_Text>((int)UI_HUD.Texts.BombCooltimeText).text = string.Format("{0:0.0}", CurrentCooltime);
            hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Bomb).size = CurrentCooltime / Cooltime;

            CurrentCooltime -= Time.deltaTime;
            yield return null;
        }

        hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Bomb).GetComponent<Image>().color = Color.white;
        hud.Get<TMP_Text>((int)UI_HUD.Texts.BombCooltimeText).gameObject.SetActive(false);

        CurrentCooltime = 0;
    }
}
