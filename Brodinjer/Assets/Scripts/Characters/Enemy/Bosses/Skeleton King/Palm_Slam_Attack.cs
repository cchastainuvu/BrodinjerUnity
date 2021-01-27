using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palm_Slam_Attack : Enemy_Attack_Base
{

    public string xPositionName, zPositionName;
    public string animationTriggerName, AnimationAttackTriggerName, AnimationIdleTriggerName;
    public Transform Corner01, Corner02;
    private float x, z;
    private float minX, maxX, minZ, maxZ;
    public GameObject DamageObj;
    public float PauseTime;

    private void Awake()
    {
        DamageObj.SetActive(false);
        if (Corner01.position.x > Corner02.position.x)
        {
            maxX = Corner01.position.x;
            minX = Corner02.position.x;
        }
        else
        {
            maxX = Corner02.position.x;
            minX = Corner01.position.x;
        }

        if (Corner01.position.z > Corner02.position.z)
        {
            maxZ = Corner01.position.z;
            minZ = Corner02.position.z;
        }
        else
        {
            maxZ = Corner02.position.z;
            minZ = Corner01.position.z;
        }
    }

    public override IEnumerator Attack()
    {
        animator.SetTrigger(animationTriggerName);
        yield return new WaitForSeconds(AttackStartTime);
        SetPosition();
        yield return new WaitForSeconds(PauseTime);
        animator.SetTrigger(AnimationAttackTriggerName);
        WeaponAttackobj.SetActive(true);
        yield return new WaitForSeconds(AttackActiveTime);
        WeaponAttackobj.SetActive(false);
        DamageObj.SetActive(true);
        float currentTime = CoolDownTime;
        while (currentTime > 0 && attacking)
        {
            currentTime -= .05f;
            yield return new WaitForSeconds(.05f);
        }

        if (currentTime <= 0)
        {
            animator.SetTrigger(AnimationIdleTriggerName);
        }

        DamageObj.SetActive(false);
    }

    public void SetPosition()
    {
        Vector3 position = player.position;
        x = Mathf.Clamp(position.x, minX, maxX);
        z = Mathf.Clamp(position.z, minZ, maxZ);
        x = GeneralFunctions.ConvertRange(minX, maxX, -1, 1, x);
        z = GeneralFunctions.ConvertRange(minZ, maxZ, -1, 1, z);
        animator.SetFloat(xPositionName, x);
        animator.SetFloat(zPositionName, z);
    }
}
