using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropObjects : MonoBehaviour
{
    public GameObject Explosion;

    public int health;
    public bool isExplosive;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
            if (isExplosive)
                Instantiate(Explosion, transform.position, transform.rotation);

        }
    }
}
