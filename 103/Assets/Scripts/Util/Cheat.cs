using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    PlayerController player;
    public static bool invinCheat;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                Destroy(enemys[i]);
            }

            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            cheatUI.SetInfo("��� �� ����");
            Destroy(cheatUI.gameObject, 0.5f);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            player.damage = 8f;
            player.fireIdx = 4;

            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            cheatUI.SetInfo("���� ���׷��̵� �ְ� �ܰ�� ���");
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            player.Init();
            UI_HUD hud = UIManager.Instance.SceneUI as UI_HUD;
            hud.InitSkillUI();

            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            cheatUI.SetInfo("��ų �ʱ�ȭ");
        }
        else if(Input.GetKeyDown(KeyCode.F4))
        {
            player.hp = player.maxHp; 
            
            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            cheatUI.SetInfo("������ �ʱ�ȭ");
        }
        else if(Input.GetKeyDown(KeyCode.F5))
        {
            player.fuel = player.maxFuel;
            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            cheatUI.SetInfo("���� �ʱ�ȭ");
        }
        else if(Input.GetKeyDown(KeyCode.F6))
        {

            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            cheatUI.SetInfo("�������� �̵�");

            if (GameManager.Instance.CurrentStage == 1)
            {
                SceneManagerEx.Instance.Load(Define.Scene.Stage2);
            }
            else
            {
                SceneManagerEx.Instance.Load(Define.Scene.Stage1);
            }
        }
        else if(Input.GetKeyDown(KeyCode.F7))
        {
            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();
            
            if (invinCheat)
            {
                invinCheat = false;
                cheatUI.SetInfo("���� ��Ȱ��ȭ");
            }
            else
            {
                invinCheat = true;
                cheatUI.SetInfo("���� Ȱ��ȭ");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            UI_Cheat cheatUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Cheat")).GetComponent<UI_Cheat>();

            player.attackSpeed = 0.1f;
            cheatUI.SetInfo("���� �ӵ� �ְ� �ܰ�� ���");
        }
    }
}
