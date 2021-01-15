using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase currentWeapon;

    public void WeaponDisable()
    {
        if(currentWeapon != null)
            currentWeapon.End();
    }

    public void WeaponEnable()
    {
        if(currentWeapon != null)
            currentWeapon.Initialize();
    }

    public void WeaponFreeze()
    {
        if(currentWeapon != null)
            currentWeapon.Freeze();
    }

    public void WeaponUnfreeze()
    {
        if(currentWeapon != null)
            currentWeapon.Unfreeze();
    }

    public void SwapWeapon(WeaponBase weapon)
    {
        if(currentWeapon !=null)
            WeaponDisable();
        currentWeapon = weapon;
        if(currentWeapon != null)
            WeaponEnable();
    }

    public void PutAwayWeapon()
    {
        WeaponDisable();
        currentWeapon = null;
    }
    
    
}
