using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Attack_Melee : Enemy_Attack_Base
{
    public override IEnumerator Attack()
    {
        attacking = true;
        if(resetAnims)
            resetAnims.ResetAllTriggers();
        animations.StartAnimation();
        yield return new WaitForSeconds(AttackStartTime);
        WeaponAttackobj.SetActive(true);
        yield return new WaitForSeconds(AttackActiveTime);
        WeaponAttackobj.SetActive(false);
        animations.StopAnimation();
        yield return new WaitForSeconds(CoolDownTime);
        attacking = false;

    }
}
