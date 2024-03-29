using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_SpawnPool : SpawnPool
{
    [SerializeField] GameObject[] enemyOrigins = new GameObject[6];

    protected override void Init()
    {
        base.Init();
        currentTime = 0;
        spawnTime = 1f;
        //90 100���� ����
        exitTime = 80f;
        enemyOrigins[0] = Resources.Load<GameObject>("Prefabs/Enemy/Meteor");
        enemyOrigins[1] = Resources.Load<GameObject>("Prefabs/Enemy/CaseShot_Dron");
        enemyOrigins[2] = Resources.Load<GameObject>("Prefabs/Enemy/Self_MeleeAilen");
        enemyOrigins[3] = Resources.Load<GameObject>("Prefabs/Enemy/Dia_MeleeAilen");
        enemyOrigins[4] = Resources.Load<GameObject>("Prefabs/Enemy/Double_Dron");
        enemyOrigins[5] = Resources.Load<GameObject>("Prefabs/Enemy/Straight_Dron");
    }

    protected override IEnumerator CoSpawn()
    {
        int spawn = Random.Range(0, spawnPoint.Length);
        GameObject go = null;
        yield return new WaitForSeconds(1f);
        spawn = Random.Range(0, spawnPoint.Length);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromSecondStage.CaseShot_Dron]);
        isExist[0] = true;
        go.transform.position = spawnPoint[0].position;
        yield return new WaitForSeconds(spawnTime);
        spawn = Random.Range(0, spawnPoint.Length);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromSecondStage.Double_Dron]);
        go.transform.position = spawnPoint[2].position;
        isExist[2] = true;
        yield return new WaitForSeconds(spawnTime);
        spawn = Random.Range(0, spawnPoint.Length);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromSecondStage.Self_MeleeAilen]);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);

        while (exitTime > currentTime)
        {
            bool isDron = false;
            int enemyIdx = Random.Range(0, enemyOrigins.Length);
            int spawnIdx = Random.Range(0, spawnPoint.Length);
            GameObject origin = enemyOrigins[enemyIdx];
            LongDistanceEnemyController longDistanceEnemy = origin.GetComponent<LongDistanceEnemyController>();
            
            if (longDistanceEnemy != null)
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

            go = SpawnManager.Instance.Spawn(origin);
            go.transform.position = spawnPoint[spawnIdx].position;

            if (isDron)
            {
                longDistanceEnemy.SetPoint(spawnIdx);
            }

            yield return new WaitForSeconds(spawnTime);
        }

        UIManager.Instance.ShowPopupUI<UI_Warning>();
    }
}
