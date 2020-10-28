using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_Magic : Trigger_Event_Base
{
    private GameObject obj;
    public float freezeTime;
    private PlayerMovement playermove;
    private WeaponManager weapons;
    private Coroutine resetFunc;

    private void OnTriggerEnter(Collider other)
    {
        obj = other.gameObject;
        StartCoroutine(CheckTrigger(other));
    }

    public override void RunEvent()
    {
        base.RunEvent();
        playermove = obj.GetComponent<PlayerMovement>();
        weapons = obj.GetComponent<WeaponManager>(); 
        if (playermove)
        {
            playermove.StopAll();
        }

        if (weapons)
        {
            weapons.WeaponFreeze();
        }
        resetFunc = StartCoroutine(ResetMove());

    }

    private IEnumerator ResetMove()
    {
        yield return new WaitForSeconds(freezeTime);
        if (playermove)
        {
            playermove.StartAll();
        }

        if (weapons)
        {
            weapons.WeaponUnfreeze();
        }
    }

    public void Unfreeze()
    {
        if(resetFunc!= null)
            StopCoroutine(resetFunc);
        if (playermove)
        {
            playermove.StartAll();
        }

        if (weapons)
        {
            weapons.WeaponUnfreeze();
        }
    }
}
