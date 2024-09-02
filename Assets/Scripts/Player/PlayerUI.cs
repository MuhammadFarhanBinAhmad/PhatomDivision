using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    PlayerHealth s_PlayerHealth;
    PlayerWeaponManager s_PlayerWeaponManager;
    PlayerSkills s_PlayerSkills;

    [Header("HealthUI")]
    public TextMeshProUGUI t_CurrHealth;
    public Image i_CurrHealth;
    public TextMeshProUGUI t_CurrShield;
    public Image i_CurrShield;
    [Header("WeaponUI")]
    public TextMeshProUGUI t_CurrMagCount;
    public TextMeshProUGUI t_WeaponName;
    public Image i_ReloadBar;
    public TextMeshProUGUI t_CurrGrenadeRemaining;
    [Header("EXPUI")]
    public Image i_EXP;
    public TextMeshProUGUI t_EXP;
    public TextMeshProUGUI t_CurrentLevel;
    private void Awake()
    {
        s_PlayerHealth = FindObjectOfType<PlayerHealth>();
        s_PlayerWeaponManager = FindObjectOfType<PlayerWeaponManager>();
        s_PlayerSkills = FindObjectOfType<PlayerSkills>();
    }

    public void UpdateHealthUI()
    {
        t_CurrHealth.text = "Health: " + s_PlayerHealth.p_Health.ToString() + '/' + s_PlayerHealth.p_MaxHealth.ToString();
        t_CurrShield.text = "Shield: " + s_PlayerHealth.p_Shield.ToString() + '/' + s_PlayerHealth.p_MaxShield.ToString();
    }
    public void UpdateAmmoUI(Weapon w)
    {
        t_CurrMagCount.text = "Ammo: " + w.p_CurrMagCount.ToString() + '/' + w.p_CurrAmmo.ToString();
        t_WeaponName.text = w.p_WeaponName;
    }
    public void UpdateGrenadeUI()
    {
        t_CurrGrenadeRemaining.text = "x" + s_PlayerWeaponManager.p_GrenadeRemaining;
    }
    public void UpdateEXP()
    {
        t_EXP.text = s_PlayerSkills.totalExpPoint.ToString() + '/' + s_PlayerSkills.nextPointToLevelUp.ToString();
        i_EXP.fillAmount = (float)s_PlayerSkills.totalExpPoint / (float)s_PlayerSkills.nextPointToLevelUp;
        print((float)s_PlayerSkills.totalExpPoint / (float)s_PlayerSkills.nextPointToLevelUp);
        t_CurrentLevel.text = "Level: " + s_PlayerSkills.currentLevel.ToString();
    }
    public void ReloadAnim(Weapon w)
    {
        i_ReloadBar.fillAmount = s_PlayerWeaponManager.p_ReloadTimeLeft / w.p_ReloadTime;
        print(s_PlayerWeaponManager.p_ReloadTimeLeft / w.p_ReloadTime);
    }
}
