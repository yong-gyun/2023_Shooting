using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Stage1_SpawnPool : SpawnPool
{
    GameObject[] enemyOrigins = new GameObject[6];

    protected override void Init()
    {
        exitTime = 80f;
        spawnTime = 1.5f;
        currentTime = 0f;
        enemyOrigins[0] = Resources.Load<GameObject>("Prefabs/Enemy/Straight_Dron");
        enemyOrigins[1] = Resources.Load<GameObject>("Prefabs/Enemy/Dia_MeleeAilen");
        enemyOrigins[2] = Resources.Load<GameObject>("Prefabs/Enemy/Cross_MeleeAilen");
        enemyOrigins[3] = Resources.Load<GameObject>("Prefabs/Enemy/Sector_Dron");
        enemyOrigins[4] = Resources.Load<GameObject>("Prefabs/Enemy/Meteor");
        enemyOrigins[5] = Resources.Load<GameObject>("Prefabs/Enemy/FarAilen");


        base.Init();
    }

    protected override IEnumerator CoSpawn()
    {
        int spawn = Random.Range(0, spawnPoint.Length);
        GameObject go = null;
        yield return new WaitForSeconds(1f);
        go = GameManager.Instance.Spawn(Define.WorldObject.Meteor);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);
        go = GameManager.Instance.Spawn(Define.WorldObject.Dia_MeleeAilen);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);
        go = GameManager.Instance.Spawn(Define.WorldObject.Cross_MeleeAilen);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);
        go = GameManager.Instance.Spawn(Define.WorldObject.Sector_Dron);
        isExist[1] = true;
        go.transform.position = spawnPoint[1].position;
        yield return new WaitForSeconds(spawnTime);
        go = GameManager.Instance.Spawn(Define.WorldObject.FarAilen);
        isExist[3] = true;
        go.transform.position = spawnPoint[3].position;
        yield return new WaitForSeconds(spawnTime);

        while(exitTime > currentTime)
        {
            bool isDron = false;
            int enemyIdx = Random.Range(0, enemyOrigins.Length);
            int spawnIdx = Random.Range(0, spawnPoint.Length);
            Define.WorldObject type = GameManager.Instance.GetWorldObjectType(enemyOrigins[enemyIdx]);
            
            if(type == Define.WorldObject.Sector_Dron || type == Define.WorldObject.Straight_Dron || type == Define.WorldObject.CaseShot_Dron || type == Define.WorldObject.Double_Dron ||type == Define.WorldObject.FarAilen)
            {
                if (isExist[spawnIdx])
                {
                    continue;
                }
                else
                {
                    isDron = true;
                    isExist[spawnIdx] = true;
                }
            }

            go = GameManager.Instance.Spawn(type);
            go.transform.position = spawnPoint[spawnIdx].position;

            if(isDron)
            {
                go.GetComponent<DronController>().SetPoint(spawnIdx);
            }

            yield return new WaitForSeconds(spawnTime);
        }

        UIManager.Instance.ShowPopupUI<UI_Waring>();
    }
}
