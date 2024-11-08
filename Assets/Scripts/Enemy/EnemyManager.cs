using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyMovement> enemy_List;

    public void AlertAll()
    {
        for (int i =0;i < enemy_List.Count ;i++)
        {
            enemy_List[i].m_AlertValue = 100;
            if (enemy_List[i].p_Mode != EnemyMovement.MODE.ATTACKING)
            {
                enemy_List[i].p_Mode = EnemyMovement.MODE.SEARCHING;
            }
        }
    }
    public void AlertPosition()
    {
        for (int i = 0; i < enemy_List.Count; i++)
        {
            if (enemy_List[i].m_Target == null)
            {
                enemy_List[i].m_Target = FindObjectOfType<PlayerHealth>().transform;
            }
            enemy_List[i].m_LastPlayerLocation = enemy_List[i].m_Target.position;
        }
    }
}
