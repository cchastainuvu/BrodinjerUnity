using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rib_Cage_Drop_Attack : Enemy_Attack_Base
{
    private bool currentlyattacking;

    public override void Init()
    {
        base.Init();
        WeaponAttackobj.SetActive(false);
    }

    public override IEnumerator Attack()
    {
        currentlyattacking = false;
        while (attacking)
        {
            if (!currentlyattacking)
            {
                if (animations != null)
                    animations.StartAnimation();
                currentlyattacking = true;
                yield return new WaitForSeconds(AttackStartTime);
                WeaponAttackobj.SetActive(true);
                yield return new WaitForSeconds(AttackActiveTime);
                WeaponAttackobj.SetActive(false);
                yield return new WaitForSeconds(CoolDownTime);
                currentlyattacking = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

}
