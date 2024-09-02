using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackBehaviour : MonoBehaviour
{

    public float e_RateOfAttack;
    float nexttime_ToFire;

    [Header("ExplosiveEnemy")]
    public bool ExplosiveEnemy;
    public GameObject Explosion;

    [Header("MeleeEnemy")]
    Animator animator;
    bool Attacking;

    private void Start()
    {
        if (!ExplosiveEnemy)
            animator = GetComponent<Animator>();
    }
    public void AttackPlayer()
    {
        if (ExplosiveEnemy)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            if (Time.time >= nexttime_ToFire)
            {
                nexttime_ToFire = Time.time + 1f / e_RateOfAttack;
                animator.SetTrigger("Attacking");
            }
        }

    }
}
