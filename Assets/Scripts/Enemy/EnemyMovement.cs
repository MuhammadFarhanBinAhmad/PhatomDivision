using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public enum MODE
    {
        PATROL,
        ATTACKING,
        SUSPICIOUS,
        ONALERT
    }

    EnemyRangeAttackBehaviour rangeAttackBehaviour;
    EnemyMeleeAttackBehaviour meleeAttackBehaviour;

    NavMeshAgent m_Agent;
    public MODE p_Mode;

    [Header("MovementStats")]
    public float s_MovementSpeed;

    [Header("SuspiciousStats")]
    public float sus_CurrentTime;
    public float sus_MaxTime;

    [Header("DetectionZone")]
    public Transform coneTip;  // The position of the tip of the cone (vertex)
    public float coneAngle;  // The angle at the tip of the cone (in degrees)
    public float coneLength;  // The length of the cone
    public float m_Range;
    public float m_MaxAlertValue;
    public float m_AlertValue;
    public Transform m_CenterPoint;
    public Vector3 m_LastPlayerLocation;

    public bool m_PlayerInContact;

    public Transform m_Target;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        rangeAttackBehaviour = GetComponent<EnemyRangeAttackBehaviour>();
        meleeAttackBehaviour = GetComponent<EnemyMeleeAttackBehaviour>();

        m_Agent.speed = s_MovementSpeed;
    }
    private void Update()
    {
        DetectionCone();
        UpdateBehaviourState();

        switch (p_Mode)
        {
            case MODE.PATROL:
                {
                    if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                    {
                        Vector3 point;
                        if (RandomPoint(m_CenterPoint.position, m_Range, out point))
                        {
                            m_Agent.SetDestination(point);
                        }
                    }
                    break;
                }
            case MODE.SUSPICIOUS:
                {
                    if (sus_CurrentTime > 0)
                    {
                        m_Agent.speed = 0;
                        transform.LookAt(m_LastPlayerLocation);
                        sus_CurrentTime -= Time.deltaTime * 5;
                    }
/*                    else
                    {
                        m_Agent.speed = s_MovementSpeed;
                        p_Mode = MODE.PATROL;
                    }*/
                    break;
                }
            case MODE.ONALERT:
                {
                    m_Agent.speed = s_MovementSpeed;
                    m_Agent.SetDestination(m_LastPlayerLocation);
                    break;
                }

            case MODE.ATTACKING:
                {
                    m_Agent.speed = s_MovementSpeed * 1.25f;
                    m_Agent.SetDestination(m_Target.position);
                    if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                    {
                        if(rangeAttackBehaviour != null)
                            rangeAttackBehaviour.AttackPlayer();

                        if (meleeAttackBehaviour != null)
                            meleeAttackBehaviour.AttackPlayer();
                    }
                    break;
                }
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    void DetectionCone()
    {
        Collider[] colliders = Physics.OverlapSphere(coneTip.position, coneLength);
        foreach (Collider collider in colliders)
        {
            Vector3 directionToCollider = (collider.transform.position - coneTip.position).normalized;
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

            if (angleToCollider < coneAngle / 2)
            {
                float distanceToCollider = Vector3.Distance(coneTip.position, collider.transform.position);
                if (distanceToCollider <= coneLength)
                {
                    if (collider.GetComponent<PlayerHealth>() != null)
                    {
                        Vector3 playerpos = collider.transform.position;
                        float dist = Vector3.Distance(playerpos, transform.position);

                        if (m_AlertValue < m_MaxAlertValue)
                            m_AlertValue += Time.deltaTime * 25;
                        else if (m_AlertValue >= m_MaxAlertValue)
                            m_AlertValue = m_MaxAlertValue;

                        p_Mode = MODE.SUSPICIOUS;
                        sus_CurrentTime = sus_MaxTime;

                        m_PlayerInContact = true;
                        m_LastPlayerLocation = playerpos;
                        m_Target = collider.transform ;
                    }
                }
                else
                {
                    if (m_AlertValue > 0)
                    {
                        m_AlertValue -= Time.deltaTime * 5;
                    }
                }
            }
        }
    }

    void UpdateBehaviourState()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 forward = transform.forward * coneLength;
        Gizmos.DrawRay(coneTip.position, forward);

        // Draw the outline of the cone
        Vector3 right = Quaternion.Euler(0, coneAngle / 2, 0) * forward;
        Gizmos.DrawRay(coneTip.position, right);

        Vector3 left = Quaternion.Euler(0, -coneAngle / 2, 0) * forward;
        Gizmos.DrawRay(coneTip.position, left);
    }
}
