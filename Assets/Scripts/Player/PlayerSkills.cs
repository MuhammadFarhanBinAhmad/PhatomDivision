using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    PlayerUI s_PlayerUI;

    public int currentLevel;
    public int totalExpPoint;
    public int nextPointToLevelUp;


    private void Start()
    {
        s_PlayerUI = FindObjectOfType<PlayerUI>();
        s_PlayerUI.UpdateEXP();
    }
    public void AddExp(int exp)
    {
        totalExpPoint += exp;
        if (totalExpPoint >= nextPointToLevelUp)
        {
            currentLevel++;
            nextPointToLevelUp *= 2;
            totalExpPoint = 0;
        }

        s_PlayerUI.UpdateEXP();

    }    
}
