using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Camera mainCamera;
    PlayerUI s_PlayerUI;

    [Header("GrenadeStats")]
    public GameObject p_Grenade;
    public float p_ThrowForce;
    public int p_GrenadeRemaining;


    public List<WeaponType> so_WeaponType = new List<WeaponType> ();
    public List<Weapon> s_Weapon = new List<Weapon>();
    public GameObject p_Projectile;
    public Transform p_Spawnpos;


    public bool p_Reloading;
    public float p_ReloadTimeLeft;
    float nexttime_ToFire;

    short weaponEquipped;

    private void Start()
    {
        s_PlayerUI = FindObjectOfType<PlayerUI>();


        /*      
         *      p_StartSpecialReloadTime = p_ReloadTime * .10f;
                p_EndSpecialReloadTime = p_ReloadTime * .30f;
        */
        for(int i = 0; i < so_WeaponType.Count; i++)
        {
            SetWeapon(so_WeaponType[i], i);

        }
        //Set weapon stats
        //ChangeWeapon();


        s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        s_PlayerUI.UpdateGrenadeUI();
    }
    void SetWeapon(WeaponType wt,int numb)
    {
        s_Weapon[numb].p_WeaponFireRate = wt.FireRate;
        s_Weapon[numb].p_MaxAmmo = wt.MaxAmmo;
        s_Weapon[numb].p_MaxMagCount = wt.MaxMagCount;
        s_Weapon[numb].p_ReloadTime = wt.ReloadTime;
        s_Weapon[numb].p_BulletMaxDamage = wt.MaxDamage;
        s_Weapon[numb].p_BulletMinDamage = wt.MinDamage;
        s_Weapon[numb].p_BulletSpeed = wt.BulletSpeed;
        s_Weapon[numb].p_WeaponName = wt.WeaponName;
        s_Weapon[numb].isAuto = wt.isAuto;
        s_Weapon[numb].isDefaultWeapon = wt.isDefaultWeapon;
        s_Weapon[numb].p_Shotgun = wt.Shotgun;
        s_Weapon[numb].p_TimeBeforeSelfDestruct = wt.p_TimeBeforeSelfDestruct;

        s_Weapon[numb].p_CurrAmmo = s_Weapon[numb].p_MaxAmmo;
        s_Weapon[numb].p_CurrMagCount = s_Weapon[numb].p_MaxMagCount;

    }

    private void Update()
    {

        Rotation();

        if (s_Weapon[weaponEquipped].isAuto)
        {
            if (Input.GetButton("Fire") && Time.time >= nexttime_ToFire && s_Weapon[weaponEquipped].p_CurrMagCount > 0 && s_Weapon[weaponEquipped].p_CurrAmmo > 0)
            {
                nexttime_ToFire = Time.time + 1f / s_Weapon[weaponEquipped].p_WeaponFireRate;
                Shoot();
                s_Weapon[weaponEquipped].p_CurrMagCount--;
                s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire") && Time.time >= nexttime_ToFire && s_Weapon[weaponEquipped].p_CurrMagCount > 0 && s_Weapon[weaponEquipped].p_CurrAmmo > 0)
            {
                nexttime_ToFire = Time.time + 1f / s_Weapon[weaponEquipped].p_WeaponFireRate;
                Shoot();
                s_Weapon[weaponEquipped].p_CurrMagCount--;
                s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
            }
        }
        if (Input.GetButton("Reload") && !p_Reloading)
        {
            p_Reloading = true;
            p_ReloadTimeLeft = s_Weapon[weaponEquipped].p_ReloadTime;
        }
/*        if (Input.GetButton("Reload") && p_Reloading)
        { 
            if (p_ReloadTimeLeft >= s_Weapon[weaponEquipped].p_StartSpecialReloadTime && p_ReloadTimeLeft <= s_Weapon[weaponEquipped].p_EndSpecialReloadTime)
            {
                print("hit");
            }
        }
*/
        if (Input.GetKeyDown(KeyCode.G) && p_GrenadeRemaining > 0)
            ThrowGrenade();


            if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponEquipped = 0;
            p_Reloading = false;
            s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponEquipped = 1;
            p_Reloading = false;
            s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponEquipped = 2;
            p_Reloading = false;
            s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponEquipped = 3;
            p_Reloading = false;
            s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        }
        if (Input.GetKeyDown (KeyCode.Q))
        {
            weaponEquipped++;
            if (weaponEquipped >so_WeaponType.Count)
            {
                weaponEquipped = 0;
            }
            p_Reloading = false;
            s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        }

        if (p_Reloading)
            ReloadingWeapon();
    }

    public void Shoot()
    {
        if (s_Weapon[weaponEquipped].p_Shotgun)
        {
            for (int i = 0;i < 9;i++)
            {
                float rx = Random.Range(-5, 5);
                float ry = Random.Range(-2, 2);

                Quaternion q = Quaternion.Euler(p_Spawnpos.eulerAngles.x + rx,
                                                p_Spawnpos.eulerAngles.y + ry,
                                                p_Spawnpos.eulerAngles.z);

                GameObject S_p = Instantiate(p_Projectile, p_Spawnpos.position, q);
                float S_damage = Random.Range(s_Weapon[weaponEquipped].p_BulletMinDamage, s_Weapon[weaponEquipped].p_BulletMaxDamage);
                S_p.GetComponent<PlayerProjectile>().SetProjectileStats(s_Weapon[weaponEquipped].p_BulletSpeed, (int)S_damage);
                S_p.GetComponent<PlayerProjectile>().TimeBeforeSelfDestruct = s_Weapon[weaponEquipped].p_TimeBeforeSelfDestruct;
                S_p.GetComponent<AudioSource>().clip = so_WeaponType[weaponEquipped].GunShotAudio;
            }
        }
        else
        {
            GameObject p = Instantiate(p_Projectile, p_Spawnpos.position, p_Spawnpos.rotation);
            float damage = Random.Range(s_Weapon[weaponEquipped].p_BulletMinDamage, s_Weapon[weaponEquipped].p_BulletMaxDamage);
            p.GetComponent<PlayerProjectile>().SetProjectileStats(s_Weapon[weaponEquipped].p_BulletSpeed, (int)damage);
            p.GetComponent<PlayerProjectile>().TimeBeforeSelfDestruct = s_Weapon[weaponEquipped].p_TimeBeforeSelfDestruct;
            p.GetComponent<AudioSource>().clip = so_WeaponType[weaponEquipped].GunShotAudio;
        }

    }
    public void ReloadingWeapon()
    {
        if (p_ReloadTimeLeft >=0)
        {
            p_ReloadTimeLeft -= Time.deltaTime;
            s_PlayerUI.ReloadAnim(s_Weapon[weaponEquipped]);
        }
        else
        {
            int ammoNeeded = s_Weapon[weaponEquipped].p_MaxMagCount - s_Weapon[weaponEquipped].p_CurrMagCount;

            //Enough for full mag
            if(ammoNeeded <= s_Weapon[weaponEquipped].p_CurrAmmo)
            {
                s_Weapon[weaponEquipped].p_CurrMagCount = s_Weapon[weaponEquipped].p_MaxMagCount;
                if(!s_Weapon[weaponEquipped].isDefaultWeapon)
                {
                    s_Weapon[weaponEquipped].p_CurrAmmo -= ammoNeeded;
                }
            }
            else//Not enough for full mag. Reload with whatever is left
            {
                s_Weapon[weaponEquipped].p_CurrMagCount += s_Weapon[weaponEquipped].p_CurrAmmo;
                s_Weapon[weaponEquipped].p_CurrAmmo = 0;
            }

            p_Reloading = false;
            s_PlayerUI.UpdateAmmoUI(s_Weapon[weaponEquipped]);
        }
    }
    void ThrowGrenade()
    {
        // Instantiate the grenade at the player's position
        GameObject grenade = Instantiate(p_Grenade, p_Spawnpos.position, p_Spawnpos.rotation);

        // Add force to the grenade
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(-(transform.forward * p_ThrowForce), ForceMode.VelocityChange);

        p_GrenadeRemaining--;

        s_PlayerUI.UpdateGrenadeUI();
    }
    void Rotation()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position); // Assuming the player's plane is horizontal
        if (playerPlane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            Vector3 direction = -(mouseWorldPosition - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
    }
}
