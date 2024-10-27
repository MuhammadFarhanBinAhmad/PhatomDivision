using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    public AudioSource p_audioSource;

    bool m_alarmSoundPlayed;

    [Header("MovementStats")]
    public float s_MovementSpeed;
    public float s_changeRotateDirectionTime;
    public float s_MaxChangeRotateDirectionTime;
    public Vector3 rotationSpeed = new Vector3(0, 50, 0); // Degrees per second on each axis

    [Header("PatrolState")]
    public float m_idelTime;
    public float m_maxIdelTime;

    [Header("SuspiciousState")]
    public float m_susTime;
    public float m_maxSusTime;

    [Header("OnAlert")]
    public float m_alertTime;
    public float m_maxAlertTime;

    [Header("Attack")]
    public float m_searchTime;
    public float m_maxSearchTime;
    public float m_lookOutTime;
    public float m_maxLookOutTime;

    [Header("DetectionZone")]
    public Transform coneTip;  // The position of the tip of the cone (vertex)
    public float coneAngle;  // The angle at the tip of the cone (in degrees)
    public float coneLength;  // The length of the cone
    public float m_Range;
    public float m_MaxAlertValue;
    public float m_AlertValue;
    public Transform m_CenterPoint;


    Vector3 m_LastPlayerLocation;
    public Transform m_Target;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        rangeAttackBehaviour = GetComponent<EnemyRangeAttackBehaviour>();
        meleeAttackBehaviour = GetComponent<EnemyMeleeAttackBehaviour>();

        m_Agent.speed = s_MovementSpeed;
        m_idelTime = m_maxIdelTime;
        m_susTime = m_maxSusTime;

    }
    private void Update()
    {
        DetectionCone();

        switch (p_Mode)
        {
            case MODE.PATROL:
                {
                    if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                    {
                        //Handle rotation
                        if(s_changeRotateDirectionTime > 0)
                            s_changeRotateDirectionTime -= Time.deltaTime;
                        else
                        {
                            s_changeRotateDirectionTime = s_MaxChangeRotateDirectionTime;
                            rotationSpeed *= -1;
                        }
                        transform.Rotate(rotationSpeed);

                        if (m_idelTime > 0)
                        { 
                            m_idelTime -= Time.deltaTime;
                        }
                        else
                        {
                            Vector3 point;
                            if (RandomPoint(m_CenterPoint.position, m_Range, out point))
                            {
                                m_Agent.SetDestination(point);
                                m_idelTime = m_maxIdelTime;
                            }
                        }
                    }
                    break;
                }
            case MODE.SUSPICIOUS:
                {
                    transform.LookAt(m_LastPlayerLocation);
                    m_Agent.speed = 0;

                    if (m_susTime > 0)
                        m_susTime -= Time.deltaTime;
                    else if (m_AlertValue > 0)
                        m_AlertValue -= Time.deltaTime * 5.0f;
                    else
                    {
                        p_Mode = MODE.PATROL;
                        m_Agent.speed = s_MovementSpeed;
                        m_susTime = m_maxSusTime;
                    }
                    break;
                }
            case MODE.ONALERT:
                {
                    m_Agent.SetDestination(m_LastPlayerLocation);
                    m_Agent.speed = s_MovementSpeed;

                    if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                    {
                        //Look around
                        if (s_changeRotateDirectionTime > 0)
                            s_changeRotateDirectionTime -= Time.deltaTime;
                        else
                        {
                            s_changeRotateDirectionTime = s_MaxChangeRotateDirectionTime;
                            rotationSpeed *= -1;
                        }
                        transform.Rotate(rotationSpeed * Time.deltaTime);

                        if (m_alertTime > 0)
                            m_alertTime -= Time.deltaTime;
                        else if (m_AlertValue > 0)
                            m_AlertValue -= Time.deltaTime * 5.0f;
                        else
                        {
                            p_Mode = MODE.PATROL;
                            m_alertTime = m_maxAlertTime;
                        }
                    }

                    break;
                }
                case MODE.ATTACKING:
                {
                    //When player is out of enemy viewrange
                    if (m_searchTime > 0)
                    {
                        m_searchTime -= Time.deltaTime;
                        m_Agent.SetDestination(m_Target.position);
                        m_LastPlayerLocation = m_Target.position;
                        //Attacking player
                        if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                        {
                            if (rangeAttackBehaviour != null)
                                rangeAttackBehaviour.AttackPlayer();

                            if (meleeAttackBehaviour != null)
                                meleeAttackBehaviour.AttackPlayer();
                        }
                    }
                    else
                    {
                        m_Agent.SetDestination(m_LastPlayerLocation);
                        if (m_lookOutTime > 0)
                        {
                            m_lookOutTime -= Time.deltaTime;
                        }
                        else if (m_AlertValue > 0)
                            m_AlertValue -= Time.deltaTime * 5.0f;
                        else
                        {
                            p_Mode = MODE.PATROL;
                            m_searchTime = m_maxSearchTime;
                            m_lookOutTime = m_maxLookOutTime;
                        }
                        //Look around
                        if (s_changeRotateDirectionTime > 0)
                            s_changeRotateDirectionTime -= Time.deltaTime;
                        else
                        {
                            s_changeRotateDirectionTime = s_MaxChangeRotateDirectionTime;
                            rotationSpeed *= -1;
                        }
                        transform.Rotate(rotationSpeed * Time.deltaTime);

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

                        UpdateBehaviourState();

                        if(!m_alarmSoundPlayed)
                            p_audioSource.Play();

                        m_LastPlayerLocation = playerpos;
                        m_Target = collider.transform ;
                    }
                }
                else
                {
                    m_alarmSoundPlayed = false;
                }

            }
        }
    }
    void UpdateBehaviourState()
    {
        if (m_AlertValue > 0.0f && m_AlertValue <= 25.0f)
        {
            p_Mode = MODE.SUSPICIOUS;
        }
        if (m_AlertValue > 25.0f && m_AlertValue <= 90.0f)
        {
            p_Mode = MODE.ONALERT;
        }
        if (m_AlertValue > 90.0f && m_AlertValue <= 100.0f)
        {
            p_Mode = MODE.ATTACKING;
            m_searchTime = m_maxSearchTime;
            m_lookOutTime = m_maxLookOutTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.green;
        Vector3 forward = transform.forward * coneLength;
        Gizmos.DrawRay(coneTip.position, forward);

        // Draw the outline of the cone
        Vector3 right = Quaternion.Euler(0, coneAngle / 2, 0) * forward;
        Gizmos.DrawRay(coneTip.position, right);

        Vector3 left = Quaternion.Euler(0, -coneAngle / 2, 0) * forward;
        Gizmos.DrawRay(coneTip.position, left);
    }
}
