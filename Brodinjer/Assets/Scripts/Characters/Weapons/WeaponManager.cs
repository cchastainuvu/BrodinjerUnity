using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase currentWeapon;
    private bool freeze;
    public SoundController Equip, Unequip;
    public void WeaponDisable()
    {
        if(currentWeapon != null)
            currentWeapon.End();
    }

    public void WeaponEnable()
    {
        if (!freeze)
        {
            if (currentWeapon != null)
                currentWeapon.Initialize();
        }
    }

    public void WeaponOff()
    {
        if (currentWeapon != null)
            currentWeapon.Off();
    }

    public void WeaponOn()
    {
        if (currentWeapon != null)
            currentWeapon.On();
    }

    public void WeaponFreeze()
    {
        freeze = true;
        if(currentWeapon != null)
            currentWeapon.Freeze();
    }

    public void WeaponUnfreeze()
    {
        freeze = false;
        if(currentWeapon != null)
            currentWeapon.Unfreeze();
    }

    public void SwapWeapon(WeaponBase weapon, bool initialize)
    {
        if(currentWeapon !=null)
            WeaponDisable();
        currentWeapon = weapon;
        if (initialize)
        {
            Equip.Play();
            weapon.Initialize();
        }
        else
            weapon.Activate();
        if(currentWeapon != null)
            WeaponEnable();
    }

    public void PutAwayWeapon()
    {
        Unequip.Play();
        WeaponDisable();
        currentWeapon = null;
    }
    
    
}
