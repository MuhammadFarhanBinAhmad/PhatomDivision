using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    EnemyMovement s_EnemyMovement;

    [Header("DetectionUI")]
    public Image ui_DetectionBar;

    // Start is called before the first frame update
    void Start()
    {
        s_EnemyMovement = GetComponentInParent<EnemyMovement>();    
    }


    void UpdateEnemyAlertUI()
    {
        ui_DetectionBar.fillAmount = s_EnemyMovement.m_AlertValue / s_EnemyMovement.m_MaxAlertValue;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyAlertUI();
    }
}
