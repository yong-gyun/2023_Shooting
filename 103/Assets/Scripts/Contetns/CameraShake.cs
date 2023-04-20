using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 origin;

    private void Start()
    {
        origin = transform.position;
    }

    public void OnShake(float magnitude, float time)
    {
        StartCoroutine(CoShake(magnitude, time));
    }

    IEnumerator CoShake(float magnitude, float time)
    {
        float curtime = 0;

        while(curtime <= time)
        {
            Vector3 pos = Random.insideUnitSphere;
            Camera.main.transform.position += magnitude * pos;
            curtime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = origin;
    }
}
