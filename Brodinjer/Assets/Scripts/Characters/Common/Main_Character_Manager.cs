﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Character_Manager : Character_Manager
{
    public PlayerMovement movement;
    public WeaponManager weapons;
    private Vector3 difference;
    private CharacterController _cc;
    private bool running;
    private float currentTime;

    public override void Init()
    {
        base.Init();
        if(_cc == null)
            _cc = movement.GetComponent<CharacterController>();
        running = false;

    }

    public override IEnumerator Stun(float stuntime)
    {
        movement.Stun();
        weapons.WeaponFreeze();
        yield return new WaitForSeconds(stuntime);
        movement.UnStun();
        weapons.WeaponUnfreeze();
        stunned = false;
    }

    protected override void StartKnockback(WeaponDamageAmount other)
    {
        if (_cc != null)
        {
            difference = _cc.transform.position - other.BaseObj.transform.position;
            difference.y = 0;
            difference = (difference.normalized * other.KnockbackForce);
            if(!running)
                StartCoroutine(KnockCo(other));
        }

    }

    protected virtual IEnumerator KnockCo(WeaponDamageAmount other)
    {
        running = true;
        if (_cc != null)
        {
            currentTime = other.KnockbackTime;
            while (currentTime > 0)
            {
                _cc.Move(difference * Time.deltaTime);
                currentTime -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        running = false;
    }

    private void OnDisable()
    {
        running = false;
    }
}
