using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_SpawnPool : SpawnPool
{
    GameObject[] enemyOrigins = new GameObject[6];

    protected override void Init()
    {
        exitTime = 80f;
        spawnTime = 1.5f;
        currentTime = 0f;

        string[] names = System.Enum.GetNames(typeof(Define.EnemysFromFirstStage));

        for (int i = 0; i < names.Length; i++)
        {
            enemyOrigins[i] = Resources.Load<GameObject>($"Prefabs/Enemy/{names[i]}");
        }

        base.Init();
    }

    protected override IEnumerator CoSpawn()
    {
        int spawn = Random.Range(0, spawnPoint.Length);
        GameObject go = null;
        yield return new WaitForSeconds(1f);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromFirstStage.Meteor]);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromFirstStage.Dia_MeleeAilen]);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromFirstStage.Cross_MeleeAilen]);
        go.transform.position = spawnPoint[spawn].position;
        yield return new WaitForSeconds(spawnTime);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromFirstStage.Sector_Dron]);
        isExist[1] = true;
        go.transform.position = spawnPoint[1].position;
        yield return new WaitForSeconds(spawnTime);
        go = SpawnManager.Instance.Spawn(enemyOrigins[(int)Define.EnemysFromFirstStage.FarAilen]);
        isExist[3] = true;
        go.transform.position = spawnPoint[3].position;
        yield return new WaitForSeconds(spawnTime);

        while(exitTime > currentTime)
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

            if(isDron)
            {
                longDistanceEnemy.SetPoint(spawnIdx);
            }

            yield return new WaitForSeconds(spawnTime);
        }

        UIManager.Instance.ShowPopupUI<UI_Warning>();
    }
}
