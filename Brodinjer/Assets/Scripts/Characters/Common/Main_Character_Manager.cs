using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Character_Manager : Character_Manager
{
    public PlayerMovement movement;
    public WeaponManager weapons;
    public override IEnumerator Stun(float stuntime)
    {
        movement.Stun();
        weapons.WeaponFreeze();
        yield return new WaitForSeconds(stuntime);
        movement.UnStun();
        weapons.WeaponUnfreeze();
        stunned = false;
    }
}
