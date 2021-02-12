using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwingTriggerEvent : Trigger_Event_Base
{
    public RandomSoundController MeleeRandom;
    public SoundController StrongMelee;
    public MeleeWeapon melee;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(gameObject.name + " " + other.gameObject.name + other.gameObject.tag);
        StartCoroutine(CheckTrigger(other));
    }

    public override void RunEvent()
    {
        base.RunEvent();
        if (melee.largeAttack)
            StrongMelee.Play();
        else
            MeleeRandom.Play();
    }
}
