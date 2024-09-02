using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    PlayerUI s_PlayerUI;

    public int p_Health;
    public int p_MaxHealth;
    public int p_Shield;
    public int p_MaxShield;



    private void Start()
    {
        s_PlayerUI = FindObjectOfType<PlayerUI>();
        s_PlayerUI.UpdateHealthUI();
    }
    public void TakeDamage(int dmg)
    {
        if (p_Shield >0)
        {
            p_Shield -= dmg;
            if (p_Shield <= 0)
                p_Shield = 0;

        }
        else
        {
            if (p_Health >0)
                p_Health -= dmg;

        }
        s_PlayerUI.UpdateHealthUI();
    }
}
