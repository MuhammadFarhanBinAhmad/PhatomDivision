using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int AmountOfExpDrop;

    public GameObject DeathSoundObject;
    public GameObject ExpDrop;
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Instantiate(DeathSoundObject);
            FindObjectOfType<CameraControl>().SlowDownEffect();
            for (int i = 0; i < AmountOfExpDrop; i++)
            {
                GameObject exp = Instantiate(ExpDrop,transform.position,transform.rotation);
                Rigidbody rb = exp.GetComponent<Rigidbody>();
                Vector3 explosionDirection = Random.insideUnitSphere.normalized;
                if (rb!=null)
                {
                    rb.AddForce(explosionDirection * .5f, ForceMode.Impulse);
                }
            }

            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<CameraControl>().ShakeCamera(1f, .1f);
        }
    }
}
