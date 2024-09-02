using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float p_BlastRadius;
    public int p_Damage;
    public float p_TimeBeforSelfDestruct;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = p_BlastRadius;
        Invoke("DestroySelf",p_TimeBeforSelfDestruct);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyHealth>() != null)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(p_Damage);
            //Damage enemy
        }

        if (other.GetComponent<PropObjects>() != null)
        {
            other.GetComponent<PropObjects>().TakeDamage(p_Damage);
        }
        if (other.GetComponent<PlayerHealth>() != null)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(p_Damage);
        }
    }    


}
