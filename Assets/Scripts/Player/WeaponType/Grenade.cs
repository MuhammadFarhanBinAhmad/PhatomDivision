using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float s_TimeBeforeExplode;
    public GameObject s_Explosion;

    void Start()
    {
        Invoke("ExplodeGrenade", s_TimeBeforeExplode);
    }

    void ExplodeGrenade()
    {
        Instantiate(s_Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
