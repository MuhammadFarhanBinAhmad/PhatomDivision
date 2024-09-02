using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackBehaviour : MonoBehaviour
{
    public GameObject e_Projectile;
    public Transform e_SpawnPos;
    public float e_RateOfAttack;
    float nexttime_ToFire;

    public bool e_GrenadeEnemy;
    public GameObject e_Grenade;
    public float e_ThrowForce;

    public float e_ProjectileSpeed;
    public int e_ProjectileDamage;

    public void AttackPlayer()
    {
        if (Time.time >= nexttime_ToFire)
        {
            nexttime_ToFire = Time.time + 1f / e_RateOfAttack;
            if (e_GrenadeEnemy)
            {
                GameObject grenade = Instantiate(e_Grenade, e_SpawnPos.position, e_SpawnPos.rotation);

                // Add force to the grenade
                Rigidbody rb = grenade.GetComponent<Rigidbody>();
                rb.AddForce((transform.forward * e_ThrowForce), ForceMode.VelocityChange);
            }
            else
            {
                GameObject p = Instantiate(e_Projectile, e_SpawnPos.position, e_SpawnPos.rotation);
                e_Projectile.GetComponent<EnemyProjectile>().SetProjectileStats(e_ProjectileSpeed, e_ProjectileDamage);
            }

        }
    }
}
