using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string p_WeaponName;

    [Header("FireRate")]
    public float p_WeaponFireRate;

    [Header("AmmoAndMagazine")]
    public int p_CurrAmmo;
    public int p_MaxAmmo;
    public int p_CurrMagCount;
    public int p_MaxMagCount;

    [Header("ReloadStats")]
    public float p_ReloadTime;
    public float p_StartSpecialReloadTime;
    public float p_EndSpecialReloadTime;

    [Header("ProjectileStats")]
    public float p_BulletSpeed;
    public float p_BulletMaxDamage;
    public float p_BulletMinDamage;
    public float p_TimeBeforeSelfDestruct;

    public bool isAuto;
    public bool isDefaultWeapon;
    public bool p_Shotgun;
}
