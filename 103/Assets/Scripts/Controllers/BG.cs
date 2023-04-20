using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    Material mat;
    float speed = 1f;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset.y -= speed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
