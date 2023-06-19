using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : UI_Scene
{
    public enum Scrollbars
    {
        Repair,
        Bomb,
        HP,
        Fuel,
    }

    public enum Texts
    {
        FuelText,
        HPText,
        BombUseCountText,
        BombCooltimeText,
        RepairUseCountText,
        RepairCooltimeText,
        StageText,
        ScoreText,
        GoldText,
        TimeText
    }

    PlayerController player;

    protected override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Scrollbar>(typeof(Scrollbars));
        Get<Scrollbar>((int)Scrollbars.Repair).size = 0;
        Get<Scrollbar>((int)Scrollbars.Bomb).size = 0;
        Get<TMP_Text>((int)Texts.RepairCooltimeText).gameObject.SetActive(false);
        Get<TMP_Text>((int)Texts.BombCooltimeText).gameObject.SetActive(false);
        Get<TMP_Text>((int)Texts.StageText).text = $"Stage : {GameManager.Instance.CurrentStage}";
        player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();

        base.Init();
    }

    public void InitSkillUI()
    {
        Get<Scrollbar>((int)Scrollbars.Repair).size = 0;
        Get<Scrollbar>((int)Scrollbars.Bomb).size = 0;
        Get<TMP_Text>((int)Texts.RepairCooltimeText).gameObject.SetActive(false);
        Get<TMP_Text>((int)Texts.BombCooltimeText).gameObject.SetActive(false);
        Get<TMP_Text>((int)Texts.RepairUseCountText).text = "5";
        Get<TMP_Text>((int)Texts.BombUseCountText).text = "5";
    }

    private void Update()
    {
        Get<Scrollbar>((int)Scrollbars.HP).size = player.hp / player.maxHp;
        Get<Scrollbar>((int)Scrollbars.Fuel).size = player.fuel / player.maxFuel;
        Get<TMP_Text>((int)Texts.HPText).text = $"{(int)player.hp} / {(int)player.maxHp}";
        Get<TMP_Text>((int)Texts.FuelText).text = $"{(int)player.fuel} / {(int)player.maxFuel}";
        Get<TMP_Text>((int)Texts.GoldText).text = $"Gold : {GameManager.Instance.CurrentGold}";

        int min = (int)GameManager.Instance.CurrentTime / 60;
        int sec = (int)GameManager.Instance.CurrentTime - min * 60;
        Get<TMP_Text>((int)Texts.TimeText).text = $"{min} : {sec}";
        Get<TMP_Text>((int)Texts.ScoreText).text = $"Score : {GameManager.Instance.Score}";
    }
}
