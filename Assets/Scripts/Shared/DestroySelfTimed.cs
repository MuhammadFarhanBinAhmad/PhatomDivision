using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfTimed : MonoBehaviour
{
    public float TimeBeforeDestroy;

    private void Start()
    {
        Invoke("DestroySelf",TimeBeforeDestroy);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
