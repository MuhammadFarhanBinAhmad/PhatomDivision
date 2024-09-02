using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody m_RigidBody;
    public float p_Speed;
    public int p_Damage;

    public float TimeBeforeSelfDestruct;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        Invoke("DestroySelf", TimeBeforeSelfDestruct);
    }

    private void FixedUpdate()
    {
        m_RigidBody.velocity = transform.forward * p_Speed;
    }
    public void SetProjectileStats(float speed, int damage)
    {
        p_Speed = speed;
        p_Damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
            Destroy(gameObject);


        if (other.GetComponent<PlayerHealth>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<PlayerHealth>().TakeDamage(p_Damage);
            //Damage player
        }

        if (other.GetComponent<PropObjects>() != null)
        {
            Destroy(gameObject);
            other.GetComponent<PropObjects>().TakeDamage(p_Damage);
        }
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
