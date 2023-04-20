using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Repair : Skill_Base
{
    TMP_Text useCountText;

    public override void Init()
    {
        CurrentCooltime = 0;
        UseCount = 5;
        Cooltime = 8f;
        base.Init();
    }

    public void UseSkill()
    {
        if (CurrentCooltime > 0 || UseCount == 0)
        {
            bool exist = FindObjectOfType<UI_NotUseSkill>();

            if (exist == false)
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_NotUseSkill"));

            return;
        }

        SoundManager.Instance.Play("Repair");
        PlayerController player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        player.hp += 20;

        if(player.maxHp < player.hp)
            player.hp = player.maxHp;

        UseCount--;
        hud.Get<TMP_Text>((int)UI_HUD.Texts.RepairUseCountText).text = $"{UseCount}";

        if (UseCount > 0)
        {
            StartCoroutine(CoCooltime());
        }
        else
        {
            hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Repair).size = 1;
        }
    }

    IEnumerator CoCooltime()
    {
        CurrentCooltime = Cooltime;
        hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Repair).GetComponent<Image>().color = Color.gray;
        hud.Get<TMP_Text>((int)UI_HUD.Texts.RepairCooltimeText).gameObject.SetActive(true);

        while (CurrentCooltime >= 0)
        {
            hud.Get<TMP_Text>((int)UI_HUD.Texts.RepairCooltimeText).text = string.Format("{0:0.0}", CurrentCooltime);
            hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Repair).size = CurrentCooltime / Cooltime;

            CurrentCooltime -= Time.deltaTime;
            yield return null;
        }

        hud.Get<Scrollbar>((int)UI_HUD.Scrollbars.Repair).GetComponent<Image>().color = Color.white;
        hud.Get<TMP_Text>((int)UI_HUD.Texts.RepairCooltimeText).gameObject.SetActive(false);
        CurrentCooltime = 0;
    }
}
