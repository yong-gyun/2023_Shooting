using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
    }
}
