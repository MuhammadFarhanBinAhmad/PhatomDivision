using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePropObjects : MonoBehaviour
{
    public int health;
    public GameObject Explosion;
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
