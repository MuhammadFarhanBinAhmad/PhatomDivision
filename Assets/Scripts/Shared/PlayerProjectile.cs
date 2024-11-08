using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody m_RigidBody;
    public float p_Speed;
    public int p_Damage;

    public float TimeBeforeSelfDestruct;

    public GameObject HitShotEffect;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        GetComponent<AudioSource>().Play();
        Invoke("DestroySelf", TimeBeforeSelfDestruct);
    }

    private void FixedUpdate()
    {
        m_RigidBody.linearVelocity =  -transform.forward * p_Speed;
    }
    public void SetProjectileStats(float speed, int damage)
    {
        p_Speed = speed;
        p_Damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Instantiate(HitShotEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }



        if (other.GetComponent<EnemyHealth>() != null)
        {
            Instantiate(HitShotEffect, transform.position, transform.rotation);
            other.GetComponent<EnemyHealth>().TakeDamage(p_Damage);
            Destroy(gameObject);
            //Damage enemy
        }

        if (other.GetComponent<PropObjects>() != null)
        {
            Instantiate(HitShotEffect, transform.position, transform.rotation);
            other.GetComponent<PropObjects>().TakeDamage(p_Damage);
            Destroy(gameObject);
        }
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
