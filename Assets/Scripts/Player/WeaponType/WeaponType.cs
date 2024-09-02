using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class WeaponType : ScriptableObject
{

    public AudioClip GunShotAudio;

    public string WeaponName;

    public float FireRate;
    public float ReloadTime;
    public float BulletSpeed;
    public float p_TimeBeforeSelfDestruct;

    public int MaxAmmo;
    public int MaxMagCount;
    public int MaxDamage;
    public int MinDamage;


    public bool Shotgun;
}
