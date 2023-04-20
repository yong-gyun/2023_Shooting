using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPool : MonoBehaviour
{
    public bool []isExist = new bool[4];
    [SerializeField] protected Transform []spawnPoint = new Transform[4];
    protected float currentTime;
    protected float exitTime;
    protected float spawnTime;

    private void Start()
    {
        Init();
        StartCoroutine(CoSpawn());
    }

    protected virtual void Init()
    {
        Transform spawnPoints = transform.Find("SpawnPoints");

        for (int i = 0; i < spawnPoints.childCount; i++)
        {
            spawnPoint[i] = spawnPoints.GetChild(i);
        }
    }

    protected void Update()
    {
        currentTime += Time.deltaTime;
    }

    protected abstract IEnumerator CoSpawn();
}
