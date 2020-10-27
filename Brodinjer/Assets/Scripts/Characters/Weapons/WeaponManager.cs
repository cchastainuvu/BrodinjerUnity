using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase currentWeapon;

    public void WeaponDisable()
    {
        currentWeapon.End();
    }

    public void WeaponEnable()
    {
        currentWeapon.Initialize();
    }

    public void WeaponFreeze()
    {
        currentWeapon.Freeze();
    }

    public void WeaponUnfreeze()
    {
        currentWeapon.Unfreeze();
    }

    public void SwapWeapon(WeaponBase weapon)
    {
        WeaponDisable();
        currentWeapon = weapon;
        WeaponEnable();
    }
    
    
}
