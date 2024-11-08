using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    EnemyMovement s_EnemyMovement;

    [Header("DetectionUI")]
    public Image ui_DetectionBar;
    public TextMeshProUGUI ui_Text;

    // Start is called before the first frame update
    void Start()
    {
        s_EnemyMovement = GetComponentInParent<EnemyMovement>();    
    }


    void UpdateEnemyAlertUI()
    {
        ui_DetectionBar.fillAmount = s_EnemyMovement.m_AlertValue / s_EnemyMovement.m_MaxAlertValue;


        if (s_EnemyMovement.m_AlertValue > 0.0f && s_EnemyMovement.m_AlertValue <= 25.0f)
        {
            ui_Text.text = "SUSPICIOUS";
        }
        else if (s_EnemyMovement.m_AlertValue > 25.0f && s_EnemyMovement.m_AlertValue <= 90.0f)
        {
            ui_Text.text = "ONALERT";
        }
        else if (s_EnemyMovement.m_AlertValue > 90.0f && s_EnemyMovement.m_AlertValue <= 100.0f)
        {
            if (s_EnemyMovement.m_playerSpotted)
            ui_Text.text = "ATTACKING";
            else
            ui_Text.text = "SEARCHING";
        }
        else
        {
            ui_Text.text = "PATROL";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyAlertUI();
    }
}
