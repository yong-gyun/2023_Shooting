using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    protected abstract void Init();
}
